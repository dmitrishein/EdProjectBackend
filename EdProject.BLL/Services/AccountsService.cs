using AutoMapper;
using EdProject.BLL.EmailSender;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace EdProject.BLL.Services
{
    public class AccountsService : IAccountService
    {
   
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private EmailProvider _emailService;
        private JwtProvider _jwt;
        IMapper _mapper;
        IConfiguration _config;
        public AccountsService(UserManager<User> userManager, SignInManager<User> signInManager,IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = configuration;
            _emailService = new EmailProvider(_config);
            _jwt = new JwtProvider(_config);
        }


        public async Task<TokenPairModel> SignInAsync(LoginModel userSignInModel)
        {
            LoginModelValidation(userSignInModel);

            var user = await _userManager.FindByEmailAsync(userSignInModel.Email);

            UserCheck(user);

            TokenPairModel tokenPairModel = new TokenPairModel
            {
                AccessToken = _jwt.GenerateAccessToken(user),
                RefreshToken = _jwt.GenerateRefreshToken()
            };

            await _signInManager.PasswordSignInAsync(user.UserName, userSignInModel.Password, userSignInModel.RememberMe, false);

            return tokenPairModel;
        }
        public async Task SignOutAsync()
        {          
          
          await _signInManager.SignOutAsync();
        }
        public async Task RegisterUserAsync(UserCreateModel userModel)
        {
            RegistrationModelValidation(userModel);

            var newUser = _mapper.Map<UserCreateModel, User>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
            {
                throw new CustomException($"Registration failed. Possible reasons:{result}", HttpStatusCode.BadRequest);
            }
            
            await _userManager.AddToRoleAsync(newUser,"Client");
            await ConfirmAccountAsync(newUser);
      
        }

        public async Task ConfirmAccountAsync(User user)
        {
            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            EmailModel emailMessage = new()
            {
                Email = user.Email,
                Message = $"https://localhost:44386/Account/ChangePassword?token={confirmToken}&email={user.Email}",
                Subject = "Account confirm"
            };
            await SendEmail(emailMessage);
        }
        public async Task SendEmail(EmailModel emailModel)
        {
            await _emailService.SendEmailAsync(emailModel);    
        }
        public async Task ConfirmEmailAsync(EmailValidationModel validationModel)
        {       
             var user = await _userManager.FindByEmailAsync(validationModel.Email);
            

             await _userManager.ConfirmEmailAsync(user, validationModel.Token);    
        }

        public async Task ResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            EmailModel emailMessage = new()
            {
                Email = email,
                Message = $"https://localhost:44386/Account/ChangePassword?token={resetToken}&email={email}",
                Subject = "Reset Password"
            };

            await SendEmail(emailMessage);     
        }
        public async Task ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
             var user = await _userManager.FindByEmailAsync(changePasswordModel.Email);
             UserCheck(user);

            await _userManager.ResetPasswordAsync(user, changePasswordModel.Token, changePasswordModel.NewPassword);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IList<string>> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new CustomException(Constants.ITEM_NOT_FOUND,HttpStatusCode.NoContent);

            return await _userManager.GetRolesAsync(user);
        }


        private void RegistrationModelValidation(UserCreateModel userModel)
        {
            //email validation pattern from msdn:)
            string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            if (!userModel.ConfirmPassword.Equals(userModel.Password))
            {
                throw new CustomException("Password's doesn't match", HttpStatusCode.BadRequest);
            }
            if (!userModel.UserName.Any(char.IsLetterOrDigit) || !userModel.UserName.Trim().Any(char.IsLetterOrDigit))
            {
                throw new CustomException(Constants.INVALID_FIELD_USERNAME, HttpStatusCode.BadRequest);
            }
            if (userModel.FirstName.Any(char.IsDigit) || userModel.FirstName.Any(char.IsPunctuation) || userModel.FirstName.Any(char.IsSymbol))
            {
                throw new CustomException(Constants.INVALID_FIELD_FIRSTNAME, HttpStatusCode.BadRequest);
            }
            if (userModel.LastName.Any(char.IsDigit) || userModel.LastName.Any(char.IsSymbol))
            {
                throw new CustomException(Constants.INVALID_FIELD_LASTNAME, HttpStatusCode.BadRequest);
            }
            if (!Regex.IsMatch(userModel.Email, emailPattern, RegexOptions.IgnoreCase))
            {
                throw new CustomException(Constants.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
            }
        }
        private void LoginModelValidation(LoginModel loginModel)
        {
            //email validation pattern
            string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!loginModel.Email.Any())
                throw new CustomException(Constants.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
            if (!loginModel.Password.Any())
                throw new CustomException("Error! Enter password", HttpStatusCode.BadRequest);
            if (!Regex.IsMatch(loginModel.Email, emailPattern, RegexOptions.IgnoreCase))
                throw new CustomException(Constants.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
        }
        private void UserCheck(User user)
        {
            if (user is null)
                throw new CustomException(Constants.NOTHING_FOUND, HttpStatusCode.BadRequest);
            if (!user.EmailConfirmed)
                throw new CustomException("Email wasn't confirmed", HttpStatusCode.BadRequest);
        }
    }
}

using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.User;
using EdProject.BLL.Providers;
using EdProject.BLL.Providers.Interfaces;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly RoutingOptions _conectOption;
        private IEmailProvider _emailService;
        private IJwtProvider _jwt;
        private IMapper _mapper;
        public AccountsService(UserManager<User> userManager, SignInManager<User> signInManager,
                               IMapper mapper, IEmailProvider emailProvider, IJwtProvider jwtProvider, IOptions<RoutingOptions> options )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailService = emailProvider;
            _jwt = jwtProvider;
            _conectOption = options.Value;
        }


        public async Task<TokenPairModel> SignInAsync(LoginModel userSignInModel)
        {
            LoginModelValidation(userSignInModel);

            var user = await _userManager.FindByEmailAsync(userSignInModel.Email);
            if(user is null)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            var result = await _signInManager.PasswordSignInAsync(user, userSignInModel.Password, userSignInModel.RememberMe, false);
            if(!result.Succeeded)
            {
                throw new CustomException(result.ToString(), HttpStatusCode.BadRequest);
            }

            TokenPairModel tokenPairModel = new TokenPairModel
            {
                AccessToken = _jwt.GenerateAccessToken(user),
                RefreshToken = _jwt.GenerateRefreshToken()
            };

            return tokenPairModel;
        }
        public async Task SignOutAsync()
        {              
          await _signInManager.SignOutAsync();
        }
        public async Task RegisterUserAsync(UserCreateModel userModel)
        {
            //refactor validation
            var newUser = _mapper.Map<UserCreateModel, User>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
            {
                throw new CustomException($"{ErrorConstant.REGISTRATION_FAILED}. {result}", HttpStatusCode.BadRequest);
            }
           
            await _userManager.AddToRoleAsync(newUser,UserRolesType.Client.ToString());


            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmLink = _conectOption.ConfirmAccountRoute;
            EmailModel emailMessage = new()
            {
                Email = newUser.Email,
                Message = string.Format(confirmLink, confirmToken, newUser.Email),
                Subject = "Account confirm"
            };

            await _emailService.SendEmailAsync(emailMessage);

        }

        public async Task ConfirmEmailAsync(EmailValidationModel validationModel)
        {       
            var user = await _userManager.FindByEmailAsync(validationModel.Email);

            if(user is null)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            await _userManager.ConfirmEmailAsync(user, validationModel.Token);    
        }

        public async Task ResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = _conectOption.ResetAccountPasswordRoute;

            EmailModel emailMessage = new()
            {
                Email = email,
                Message = string.Format(resetLink, resetToken, email),
                Subject = "Reset Password"
            };

            await _emailService.SendEmailAsync(emailMessage);
        }
        public async Task ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordModel.Email);

            if(user is null)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }

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
            {
                throw new CustomException(ErrorConstant.ITEM_NOT_FOUND, HttpStatusCode.NoContent);
            }

            return await _userManager.GetRolesAsync(user);
        }

        private void LoginModelValidation(LoginModel loginModel)
        {
            //email validation pattern
            string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!loginModel.Email.Any())
                throw new CustomException(ErrorConstant.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
            if (!loginModel.Password.Any())
                throw new CustomException("Error! Enter password", HttpStatusCode.BadRequest);
            if (!Regex.IsMatch(loginModel.Email, emailPattern, RegexOptions.IgnoreCase))
                throw new CustomException(ErrorConstant.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
        }
    }
}

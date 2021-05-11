using AutoMapper;
using EdProject.BLL.EmailSender;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Policy;


namespace EdProject.BLL.Services
{
    public class AccountsService : IAccountService
    {
   
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private EmailProvider _emailService;
        IMapper _mapper;
        IConfiguration _config;
        public AccountsService(UserManager<User> userManager, SignInManager<User> signInManager,IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = configuration;
            _emailService = new EmailProvider(_config);
        }

        public async Task SignInAsync(LoginModel userSignInModel)
        {
            LoginValidation(userSignInModel);

            var user = await _userManager.FindByEmailAsync(userSignInModel.Email);

            if (user is null)
                throw new CustomException(Constant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            if (!user.EmailConfirmed)
                throw new CustomException("Email was not confirmed! Check your email", HttpStatusCode.BadRequest);

           await _signInManager.PasswordSignInAsync(user.UserName, userSignInModel.Password, userSignInModel.RememberMe, false);
        }
        public async Task SignOutAsync()
        {          
          await _signInManager.SignOutAsync();
        }
        public async Task<string> RegisterUserAsync(UserCreateModel userModel)
        {
            RegistrationValidation(userModel);

            var newUser = _mapper.Map<UserCreateModel, User>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
                throw new CustomException($"Registration failed. Possible reasons:{result}", HttpStatusCode.BadRequest); 
            
            await _userManager.AddToRoleAsync(newUser,"Client");
            return await _userManager.GenerateEmailConfirmationTokenAsync(newUser);         
        }

        public async Task SendEmail(EmailModel emailModel)
        {
            await _emailService.SendEmailAsync(emailModel);    
        }
        public async Task ConfirmEmailAsync(EmailValidationModel validationModel)
        {       
            var user = await _userManager.FindByEmailAsync(validationModel.Email);
             if (user is null)
                 throw new CustomException(Constant.USER_NOT_FOUND, HttpStatusCode.BadRequest);

            var result = await _userManager.ConfirmEmailAsync(user, validationModel.Token);    
        }

        public async Task<string> ResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null)
                throw new CustomException(Constant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            if (!user.EmailConfirmed)
                throw new CustomException("Email wasn't confirmed. Cannot recover the password", HttpStatusCode.BadRequest);

            return await _userManager.GeneratePasswordResetTokenAsync(user);      
        }
        public async Task ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
             var user = await _userManager.FindByEmailAsync(changePasswordModel.Email);
             if (user is null)
                throw new CustomException(Constant.USER_NOT_FOUND, HttpStatusCode.BadRequest);

            var result = await _userManager.ResetPasswordAsync(user, changePasswordModel.Token, changePasswordModel.NewPassword);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IList<string>> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new CustomException(Constant.USER_NOT_FOUND,HttpStatusCode.NoContent);

            return await _userManager.GetRolesAsync(user);
        }


        private void RegistrationValidation(UserCreateModel userModel)
        {
            //email validation pattern from msdn:)
            string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            if (!userModel.ConfirmPassword.Equals(userModel.Password))
                throw new CustomException("Password's doesn't match", HttpStatusCode.BadRequest);
            if (!userModel.UserName.Any(char.IsLetterOrDigit))
                throw new CustomException($"Invalid username!", HttpStatusCode.BadRequest);
            if (userModel.UserName.Any(char.IsWhiteSpace))
                throw new CustomException($"Invalid username!", HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.UserName, @"\W"))
                throw new CustomException($"Invalid username! It must consist of only numbers and letters", HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.FirstName, @"\W") || Regex.IsMatch(userModel.FirstName,@"\d") )
                throw new CustomException($"Invalid firstname! It must consist of only letters", HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.LastName, @"\W") || Regex.IsMatch(userModel.LastName, @"\d"))
                throw new CustomException($"Invalid lastname! It must consist of only letters", HttpStatusCode.BadRequest);
            if (!Regex.IsMatch(userModel.Email, emailPattern, RegexOptions.IgnoreCase))
                throw new CustomException(Constant.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
        }
        private void LoginValidation(LoginModel loginModel)
        {
            //email validation pattern
            string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!loginModel.Email.Any(char.IsLetterOrDigit))
                throw new CustomException(Constant.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
            if (!loginModel.Password.Any())
                throw new CustomException("Error! Enter password", HttpStatusCode.BadRequest);
            if (!Regex.IsMatch(loginModel.Email, emailPattern, RegexOptions.IgnoreCase))
                throw new CustomException(Constant.INCORRECT_EMAIL, HttpStatusCode.BadRequest);
        }
    }
}

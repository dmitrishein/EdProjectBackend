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

namespace EdProject.BLL.Services
{
    public class AccountsService : IAccountService
    {
   
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        IMapper _mapper;
        IConfiguration _config;
        public AccountsService(UserManager<User> userManager, SignInManager<User> signInManager,IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = configuration;
        }

        public async Task SignInAsync(LoginModel userSignInModel)
        {
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
        public async Task RegisterUserAsync(UserCreateModel userModel)
        {
            RegistrationValidation(userModel);

            var newUser = _mapper.Map<UserCreateModel, User>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);
            if (!result.Succeeded)
                throw new CustomException($"Registration failed. Possible reasons:{result.ToString()}", HttpStatusCode.BadRequest); 
            
            await _userManager.AddToRoleAsync(newUser,"Client");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmationLink = $"https://localhost:44366/Account/ConfirmEmail?token={code}&email={userModel.Email}";

            EmailModel emailConfirmationModel = new()
            {
                RecipientName = newUser.FirstName,
                Email = userModel.Email,
                Message = $"{newUser.FirstName}, follow this link to confirm your email : {confirmationLink}",
                Subject = "Account Confirmation"
            };
           
            await SendEmail(emailConfirmationModel);
              
        }
        public async Task SendEmail(EmailModel emailModel)
        {
            EmailProvider emailService = new EmailProvider(_config);

            await emailService.SendEmailAsync(emailModel);    
        }
        public async Task ConfirmEmailAsync(string token, string email)
        {       
             var user = await _userManager.FindByEmailAsync(email);

             if (user is null)
                 throw new CustomException(Constant.USER_NOT_FOUND, HttpStatusCode.BadRequest);

             await _userManager.ConfirmEmailAsync(user, token);
           
        }
        public async Task<string> PasswordRecoveryTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null)
                throw new CustomException(Constant.USER_NOT_FOUND, HttpStatusCode.BadRequest);

            return await _userManager.GeneratePasswordResetTokenAsync(user);      
        }
        public async Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
             var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);

             if (user is null)
                throw new CustomException(Constant.USER_NOT_FOUND, HttpStatusCode.BadRequest);

             await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
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

            if (!userModel.UserName.Any(char.IsLetterOrDigit))
                throw new CustomException($"Invalid username!", HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.UserName, @"\W"))
                throw new CustomException($"Invalid username! It must consist of only numbers and letters", HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.FirstName, @"\W") || Regex.IsMatch(userModel.FirstName,@"\d") )
                throw new CustomException($"Invalid firstname! It must consist of only letters", HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.LastName, @"\W") || Regex.IsMatch(userModel.LastName, @"\d"))
                throw new CustomException($"Invalid lastname! It must consist of only letters", HttpStatusCode.BadRequest);
            if (!Regex.IsMatch(userModel.Email, emailPattern, RegexOptions.IgnoreCase))
                throw new CustomException($"Invalid lastname! It must consist of only numbers and letters", HttpStatusCode.BadRequest);
        }
    }
}

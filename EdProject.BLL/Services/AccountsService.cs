using AutoMapper;
using EdProject.BLL.EmailSender;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AccountsService : IAccountService
    {
   
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        IMapper _mapper;
        public AccountsService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        

        public async Task SignInAsync(UserSignInModel userSignInModel)
        {
          var user = await _userManager.FindByEmailAsync(userSignInModel.Email);      
            
          if (user is null || !user.EmailConfirmed)
                throw new Exception("User not found");

           
          await _signInManager.PasswordSignInAsync(user.UserName, userSignInModel.Password, userSignInModel.RememberMe, false);
                   
        }
        public async Task SignOutAsync()
        {          
          await _signInManager.SignOutAsync();
        }
        public async Task RegisterUserAsync(UserCreateModel userModel)
        {
            RegistrationValidation(userModel);

            var newUser = _mapper.Map<UserCreateModel, AppUser>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);
            if (!result.Succeeded)
                throw new CustomException($"Registration failed. Possible reasons:{result.ToString()}",400); 
            
            await _userManager.AddToRoleAsync(newUser,"Client");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmationLink = $"https://localhost:44366/Account/ConfirmEmail?token={code}&email={userModel.Email}";

            EmailConfirmationModel emailConfirmationModel = new()
            {
                Email = userModel.Email,
                ConfirmationLink = confirmationLink,
                Subject = "Confirm Account"
            };
           
            await SendEmail(emailConfirmationModel);
              
        }
        public async Task SendEmail(EmailConfirmationModel emailModel)
        {
            EmailProvider emailService = new EmailProvider();

            await emailService.SendEmailAsync(emailModel.Email, $"{emailModel.Subject}",$"To confirm your account, follow the link : {emailModel.ConfirmationLink}");    
        }
        public async Task ConfirmEmailAsync(string token, string email)
        {       
             var user = await _userManager.FindByEmailAsync(email);

             if (user is null)
                 throw new CustomException("User was not found while confirm email", 400);

             await _userManager.ConfirmEmailAsync(user, token);
           
        }
        public async Task<string> PasswordRecoveryTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null)
                throw new CustomException("User with email wasn't found",400);

            return await _userManager.GeneratePasswordResetTokenAsync(user);      
        }
        public async Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
             var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);

             if (user is null)
                throw new CustomException("Cannot reset password! User wasn't found",400);

             await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
        }
        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IList<string>> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new Exception("Error! Cannot find a user!");

            return await _userManager.GetRolesAsync(user);
        }

        private void RegistrationValidation(UserCreateModel userModel)
        {
            //email validation pattern from msdn:)
            string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!userModel.UserName.Any(char.IsLetterOrDigit))
                throw new CustomException($"Invalid username!", 400);
            if (Regex.IsMatch(userModel.UserName, @"\W"))
                throw new CustomException($"Invalid username! It must consist of only numbers and letters", 400);
            if (Regex.IsMatch(userModel.FirstName, @"\W") || Regex.IsMatch(userModel.FirstName,@"\d") )
                throw new CustomException($"Invalid firstname! It must consist of only letters", 400);
            if (Regex.IsMatch(userModel.LastName, @"\W") || Regex.IsMatch(userModel.LastName, @"\d"))
                throw new CustomException($"Invalid lastname! It must consist of only letters", 400);
            if (!Regex.IsMatch(userModel.Email, emailPattern, RegexOptions.IgnoreCase))
                throw new CustomException($"Invalid lastname! It must consist of only numbers and letters", 400);
        }
    }
}

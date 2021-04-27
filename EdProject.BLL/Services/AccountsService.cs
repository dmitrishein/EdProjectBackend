using AutoMapper;
using EdProject.BLL.EmailSender;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AccountsService : IAccountService
    {
        #region UserManager, SignInManager and constructor

        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        
        public AccountsService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        public async Task<bool> SignInAsync(UserSignInModel userSignInModel)
        {
            var user = await _userManager.FindByEmailAsync(userSignInModel.Email);
           
           
            if (user is not null)
                return false;

            if (user.EmailConfirmed)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, userSignInModel.Password, userSignInModel.RememberMe,false );

                return result.Succeeded;
            }

            return false;
        }
        public async Task SignOutAsync()
        {   
            await _signInManager.SignOutAsync();
        }
        public async Task<IEnumerable<string>> RegisterUserAsync(UserRegistrationModel userModel)
        {
            
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserRegistrationModel, AppUser>());
            var _mapper = new Mapper(config); 
            var newUser = _mapper.Map<UserRegistrationModel, AppUser>(userModel);

            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
                throw new Exception($"{result.ToString()}");
            
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

            var errors = result.Errors.Select(e => e.Description);
            return errors;
    
        }
        public async Task SendEmail(EmailConfirmationModel emailModel)
        {
            EmailProvider emailService = new EmailProvider();

            await emailService.SendEmailAsync(emailModel.Email, $"{emailModel.Subject}",$"To confirm your account, follow the link : {emailModel.ConfirmationLink}");
            
        }
        public async Task<bool> ConfirmEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return true;

            return false;

        }
        public async Task<string> PasswordRecoveryAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var recoveryToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return recoveryToken;
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user is null)
                return false;

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);

            return result.Succeeded;
        }
        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IList<string>> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return await _userManager.GetRolesAsync(user);
        }

    }
}

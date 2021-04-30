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

        public async Task SignInAsync(UserSignInModel userSignInModel)
        {
          var user = await _userManager.FindByEmailAsync(userSignInModel.Email);      
            
          if (user is null || user.EmailConfirmed is false)
                throw new Exception("User not found");
          
          try
          {
             await _signInManager.PasswordSignInAsync(user.UserName, userSignInModel.Password, userSignInModel.RememberMe, false);
          }
          catch (Exception x)
          {
             throw new Exception($"Failed to login {x.Message}");
          }

        }
        public async Task SignOutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed logout",ex);
            }
        }
        public async Task RegisterUserAsync(UserCreateModel userModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserCreateModel, AppUser>());
            var _mapper = new Mapper(config); 
            var newUser = _mapper.Map<UserCreateModel, AppUser>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
                throw new Exception($"Registration failed. Possible reasons:{result.ToString()}");
            
            await _userManager.AddToRoleAsync(newUser,"Client");
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var confirmationLink = $"https://localhost:44366/Account/ConfirmEmail?token={code}&email={userModel.Email}";
            EmailConfirmationModel emailConfirmationModel = new()
            {
                Email = userModel.Email,
                ConfirmationLink = confirmationLink,
                Subject = "Confirm Account"
            };

            try
            {
              await SendEmail(emailConfirmationModel);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to confirm email in cause {ex.Message.ToString()}");
            }
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
                throw new Exception("User was not found while confirm email");

            try
            {
                await _userManager.ConfirmEmailAsync(user, token);
            }
            catch(Exception x)
            {
                throw new Exception($"Failed to confirm email {x.Message}");
            }

        }
        public async Task<string> PasswordRecoveryTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
                throw new Exception("User with email wasn't found");

            var recoveryToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return recoveryToken;
        }
        public async Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user is null)
                throw new Exception("User wasn't found while reset password");

            try 
            { 
              await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
            }
            catch(Exception x)
            {
                throw new Exception($"Password was not reset in cause {x.Message}");
            }
          
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

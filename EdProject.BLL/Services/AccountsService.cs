using AutoMapper;
using EdProject.BLL.EmailSender;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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

        public async Task<bool> SignInAsync(string password, string email, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
           
           
            if (user == null)
                return false;

            //login with confirmed email
            if (user.EmailConfirmed)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

                if (result.Succeeded)
                    return true;
            }
            return false;
        }
        public async Task SignOutAsync(string password, string email)
        {   
            await _signInManager.SignOutAsync();
        }
        public async Task<string> RegisterUserAsync(UserModel userModel)
        {
            if (userModel != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, AppUser>());
                var _mapper = new Mapper(config); 
                var newUser = _mapper.Map<UserModel, AppUser>(userModel);

                var result = await _userManager.CreateAsync(newUser, userModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser,"Client");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    return code;
                }
            }

            return null;
        }

        public async Task SendEmail(string confirmationLink, string email, string subject)
        {
            EmailProvider emailService = new EmailProvider();

            await emailService.SendEmailAsync(email, $"{subject}",$"To confirm action, follow the link : {confirmationLink}");
            
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
        public async Task<bool> ResetPasswordAsync(string token, string email,string newPasssword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if(user!=null)
            {
                var result = await _userManager.ResetPasswordAsync(user, token, newPasssword);

                if (result.Succeeded)
                    return true;
            }
            return false;
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

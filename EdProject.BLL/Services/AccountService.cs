using AutoMapper;
using EdProject.BLL.EmailSender;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AccountService : IAccountService
    {
        #region UserManager, SignInManager and constructor

        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #endregion

        public async Task<bool> Login(string password, string email, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
           
            //user validation
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
        public async Task Logout(string password, string email)
        {   
            await _signInManager.SignOutAsync();
        }
        public async Task<string> RegisterUser(UserModel userModel)
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
        public async Task SendEmailConfiramtion(string confirmationLink, string email)
        {
            EmailProvider emailService = new EmailProvider();
            await emailService.SendEmailAsync(email, "Confirm your account",$"Confirm your registration, follow the link : <a href='{confirmationLink}'>link</a>");

        }
        public async Task<bool> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return true;

            return false;

        }

    }
}

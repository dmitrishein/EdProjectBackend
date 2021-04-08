using AutoMapper;
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
                var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
                if (result.Succeeded)
                    return true;
                return false;
            }
            
            //without
            if(!user.EmailConfirmed)
            {
                return false;
            }


            return false;
        }
        public async Task Logout(string password, string email)
        {   
            await _signInManager.SignOutAsync();
        }
        public async Task RegisterUser(UserModel userModel)
        {
            //AppUser newUser = await _userManager.FindByEmailAsync(userModel.Email);
            //if user doesn't exist
            if (userModel != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, AppUser>());
                var _mapper = new Mapper(config); 
                AppUser newUser = _mapper.Map<UserModel, AppUser>(userModel);

                var result = await _userManager.CreateAsync(newUser, userModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser,"Client");

                    //send email confiramtion


                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                }

            }


            

        }

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return true;

            return false;

        }

    }
}

using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AccountService : IAccountService
    {
        #region UserManager, SignInManager and constructor
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
     

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        #endregion


        public async Task<bool> Login(string password, string email)
        {
            var result = await _signInManager.PasswordSignInAsync(email,password,false,false);
            if (result.Succeeded)
                return true;
            else
                return false;
        }
        public async Task LogOff(string password, string email)
        {   //удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
        }

        public async Task<AppUser> RegisterUser(string userName, string firstName, string lastName, string password, string email)
        {
            //создаем пользователя
            AppUser newUser = new AppUser
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
            };

            var result = await _userManager.CreateAsync(newUser, password);

            //если регистрация успешна
            if(result.Succeeded)
            {   
                await _signInManager.SignInAsync(newUser, isPersistent: false);
            }

            return newUser;
        }

        //метод получает параметры из сгенерированой ссылки,по которой перейдет пользователь
        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            //если пользователь передал неверные данные
            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return true;

            return false;

        }

       
    }
}

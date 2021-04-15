using AutoMapper;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class UserService : IUserService
    {
        #region Private Members
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        #endregion

        #region Constructor
        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        public async Task AddToRoleAsync(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user != null && await _roleManager.RoleExistsAsync(role))
            {
               await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task CreateUserAsync(UserModel userModel)
        {     
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, AppUser>());
            var _mapper = new Mapper(config);
            var newUser = _mapper.Map<UserModel, AppUser>(userModel);

            await _userManager.CreateAsync(newUser,userModel.Password);
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
                return user;

            return null;
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
                return user;

            return null;
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
                return user;

            return null;
        }
        
        public IQueryable GetAllUsers()
        {
            var users = _userManager.Users;

            return users;
        }
        public IQueryable GetUserListByRole(string roleName)
        {
            var res = _userManager.GetUsersInRoleAsync(roleName).Wait();
        }
      


        public async Task RemoveUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task UpdateUserAsync(UserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.UserName);
            
            if(user != null)
            {
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
            }
        }

    }
}

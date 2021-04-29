using AutoMapper;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (user is null)
                throw new Exception("User not exist");
            if(!await _roleManager.RoleExistsAsync(role))
            {
                throw new Exception("Wrong role. Check the rolename");
            }

            await _userManager.AddToRoleAsync(user, role);
        }
        public async Task CreateUserAsync(UserRegistrationModel userModel)
        {     
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserRegistrationModel, AppUser>());
            var _mapper = new Mapper(config);
            var newUser = _mapper.Map<UserRegistrationModel, AppUser>(userModel);

            await _userManager.CreateAsync(newUser,userModel.Password);
        }

        public async Task<AppUser> GetUserAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user != null)
            {
                return user;
            }
            return null;
        }
        public IQueryable GetAllUsers()
        {
            var users = _userManager.Users;

            return users;
        }
        public IQueryable GetAllUsersByQuery(string searchString)
        {
            var users = _userManager.Users.Where(u => u.Id.ToString() == searchString ||
                                               u.UserName == searchString ||
                                               u.FirstName == searchString ||
                                               u.LastName == searchString ||
                                               u.Email == searchString
                                               );

            return users;
        }
        public async Task<IList<AppUser>> GetUserListByRole(string roleName)
        {
            var res = await _userManager.GetUsersInRoleAsync(roleName);

            return res;
        }
        public async Task RemoveUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new Exception("user not found");
            
            await _userManager.DeleteAsync(user);
            
        }
        public async Task UpdateUserAsync(UserRegistrationModel userModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserRegistrationModel, AppUser>());
            var _mapper = new Mapper(config);
            var updatedUser = _mapper.Map<UserRegistrationModel, AppUser>(userModel);

            var oldUser = await _userManager.FindByEmailAsync(userModel.Email);

            if (!oldUser.EmailConfirmed)
                throw new Exception("user not found");

            await _userManager.UpdateAsync(updatedUser);
           
            
        }
        public async Task BlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(100));
        }
        public async Task UnblockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new Exception("user not found");
            
            await _userManager.SetLockoutEnabledAsync(user, false);
            
        }

    }
}

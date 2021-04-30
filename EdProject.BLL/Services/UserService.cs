using AutoMapper;
using EdProject.BLL.Models.User;
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
        public async Task CreateUserAsync(UserCreateModel userModel)
        {     
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserCreateModel, AppUser>());
            var _mapper = new Mapper(config);
            var newUser = _mapper.Map<UserCreateModel, AppUser>(userModel);
            newUser.EmailConfirmed = userModel.EmailConfirmed;
            await _userManager.AddToRoleAsync(newUser, "client");
            try
            {
                var res = await _userManager.FindByEmailAsync(userModel.Email);
                if (res is not null)
                    throw new Exception("User with this email already exist");
            }
            catch(Exception x)
            {
                throw new Exception($"{x.Message}");
            }

             var result = await _userManager.CreateAsync(newUser, userModel.Password);
             if (!result.Succeeded)
                throw new Exception($"{result}");
        }

        public async Task<AppUser> GetUserAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user is null)
            {
                throw new Exception("Error! User not found");
            }

            return user;
        }
        public IQueryable GetAllUsers()
        {
            return _userManager.Users;
        }
        public IQueryable GetAllUsersByQuery(string searchString)
        {
            var usersQuery = _userManager.Users.Where(u => u.Id.ToString() == searchString ||
                                               u.UserName == searchString ||
                                               u.FirstName == searchString ||
                                               u.LastName == searchString ||
                                               u.Email == searchString
                                               );

            if (usersQuery.Count() == 0)
                throw new Exception("No results! Check search parameters");

            return usersQuery;
        }
        public async Task<IList<AppUser>> GetUserListByRole(string roleName)
        {
            var result = await _userManager.GetUsersInRoleAsync(roleName);
            if (result is null)
                throw new Exception("Nobody found:(");

            return result;
        }
        public async Task RemoveUserAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new Exception("user not found");
            user.isRemoved = true;
            await _userManager.UpdateAsync(user);
            
        }
        public async Task UpdateUserAsync(UserUpdateModel userModel)
        {
            var checkUser = await _userManager.FindByIdAsync(userModel.Id.ToString());

            if (!checkUser.EmailConfirmed)
                throw new Exception("Cannot update user without confirmed email");
            if (checkUser is null)
                throw new Exception("Cannot update. User not found");
            await _userManager.SetUserNameAsync(checkUser, userModel.Username);
            checkUser.FirstName = userModel.FirstName;
            checkUser.LastName = userModel.LastName;
   
            await _userManager.UpdateAsync(checkUser);    
        }
        public async Task BlockUser(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(100));
        }
        public async Task UnblockUser(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new Exception("user not found");
            
            await _userManager.SetLockoutEnabledAsync(user, false);
            
        }

    }
}

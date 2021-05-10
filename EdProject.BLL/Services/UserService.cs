using AutoMapper;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class UserService : IUserService
    {
        #region Private Members
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        IMapper _mapper;
        #endregion

        #region Constructor
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        #endregion

        public async Task AddToRoleAsync(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new CustomException("User not exist", System.Net.HttpStatusCode.BadRequest);

            if(!await _roleManager.RoleExistsAsync(role))
            {
                throw new CustomException("Wrong role! Check the rolename", System.Net.HttpStatusCode.BadRequest);
            }

            await _userManager.AddToRoleAsync(user, role);
        }
        public async Task CreateUserAsync(UserCreateModel userModel)
        {
            if (await _userManager.FindByEmailAsync(userModel.Email) is not null)
                throw new CustomException($"User with this email already exist", System.Net.HttpStatusCode.BadRequest);
            UserValidation(userModel);
            var newUser = _mapper.Map<UserCreateModel, User>(userModel);
            newUser.EmailConfirmed = true;
            await _userManager.AddToRoleAsync(newUser, "client");

            var result = await _userManager.CreateAsync(newUser, userModel.Password);
            if (!result.Succeeded)
                throw new CustomException($"User wasn't created! {result}", System.Net.HttpStatusCode.BadRequest);
        }

        public async Task<User> GetUserAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user is null)
               throw new CustomException("Error! User not found", System.Net.HttpStatusCode.BadRequest);

            return user;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();  
        }
        public async Task<List<User>> GetAllUsersByQuery(string searchString)
        {
            var usersQuery = _userManager.Users.Where(u => u.Id.ToString() == searchString ||
                                               u.UserName == searchString ||
                                               u.FirstName == searchString ||
                                               u.LastName == searchString ||
                                               u.Email == searchString
                                               );

            if (!usersQuery.Any())
                throw new CustomException("No results! Check search parameters", System.Net.HttpStatusCode.OK);

            return await usersQuery.ToListAsync();
        }
        public async Task<IList<User>> GetUserListByRole(string roleName)
        {
            var result = await _userManager.GetUsersInRoleAsync(roleName);
            if (result is null)
                throw new CustomException("Nobody found:(", System.Net.HttpStatusCode.OK);

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
        private void UserValidation(UserCreateModel userModel)
        {
            if (!userModel.UserName.Any())
                throw new CustomException($"Username is empty!", System.Net.HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.UserName, @"\W"))
                throw new CustomException($"Invalid username! It must consist of only numbers and letters", System.Net.HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.FirstName, @"\W") || Regex.IsMatch(userModel.FirstName, @"\d"))
                throw new CustomException($"Invalid firstname! It must consist of only numbers and letters", System.Net.HttpStatusCode.BadRequest);
            if (Regex.IsMatch(userModel.LastName, @"\W") || Regex.IsMatch(userModel.LastName, @"\d"))
                throw new CustomException($"Invalid lastname! It must consist of only numbers and letters", System.Net.HttpStatusCode.BadRequest);
        }
    }
}

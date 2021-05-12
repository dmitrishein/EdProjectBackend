using AutoMapper;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task AddToRoleAsync(UserToRoleModel userToRole)
        {
            var user = await _userManager.FindByEmailAsync(userToRole.UserEmail);

            UserExistCheck(user);

            if(!await _roleManager.RoleExistsAsync(userToRole.RoleName))
            {
                throw new CustomException("Wrong role! Check the rolename", System.Net.HttpStatusCode.BadRequest);
            }

            await _userManager.AddToRoleAsync(user, userToRole.RoleName);
        }

        public async Task<UserModel> GetUserByIdAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            UserExistCheck(user);
            var userModel = _mapper.Map<User, UserModel>(user);
            return userModel;
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var usersList = _mapper.Map <List<User>, List<UserModel>> (await _userManager.Users.Where(u=> !u.isRemoved).ToListAsync());
            return usersList;  
        }
        public List<UserModel> GetUsersByQuery(string searchString)
        {
            var usersQuery = _userManager.Users.Where(u => u.Id.ToString() == searchString ||
                                                           u.UserName.Contains(searchString) ||
                                                           u.FirstName.Contains(searchString) ||
                                                           u.LastName.Contains(searchString) ||
                                                           u.Email.Contains(searchString))
                                                .Where(u => !u.isRemoved);
                                                          

            if (!usersQuery.Any())
                throw new CustomException(Constants.NOTHING_FOUND, HttpStatusCode.OK);

            var userList = _mapper.Map<IQueryable<User>, List<UserModel>>(usersQuery);

            return userList;
        }
        public async Task<List<UserModel>> GetUserListByRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            //user existing check
            var resultList = _mapper.Map<IList<User>, List<UserModel>>(users);

            return resultList;
        }
        public async Task RemoveUserAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            //user existing check
            if (user is null)
                throw new Exception("user not found");
            user.isRemoved = true;
            await _userManager.UpdateAsync(user);
            
        }
        public async Task UpdateUserAsync(UserUpdateModel userModel)
        {
            var checkUser = await _userManager.FindByIdAsync(userModel.Id.ToString());

            UserExistCheck(checkUser);
            UserUpdateValidation(userModel);

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

        private void UserUpdateValidation(UserUpdateModel userModel)
        {
            if (!userModel.Username.Any())
            {
                throw new CustomException(Constants.INVALID_FIELD_USERNAME, HttpStatusCode.BadRequest);
            }
            if (!userModel.Username.Any(char.IsLetterOrDigit) || !userModel.Username.Trim().Any())
            {
                throw new CustomException(Constants.INVALID_FIELD_USERNAME, HttpStatusCode.BadRequest);
            }
            if (userModel.FirstName.Any(char.IsDigit) || userModel.FirstName.Any(char.IsSymbol) && userModel.FirstName.Trim().Any())
            {
                throw new CustomException(Constants.INVALID_FIELD_FIRSTNAME, HttpStatusCode.BadRequest);
            }
            if (userModel.LastName.Any(char.IsDigit) || userModel.LastName.Any(char.IsSymbol))
            {
                throw new CustomException(Constants.INVALID_FIELD_LASTNAME, HttpStatusCode.BadRequest);
            }
        }
        private void UserExistCheck(User user)
        {
            if (!user.EmailConfirmed)
                throw new CustomException(Constants.NOTHING_EXIST,HttpStatusCode.BadRequest);
            if (user is null)
                throw new CustomException(Constants.NOTHING_EXIST,HttpStatusCode.BadRequest);
        }
    }
}

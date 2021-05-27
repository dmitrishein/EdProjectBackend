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
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class UserService : IUserService
    {
      
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<long>> _roleManager;
        IMapper _mapper;
      

      
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task AddToRoleAsync(UserToRoleModel userToRole)
        {
            var user = await _userManager.FindByEmailAsync(userToRole.UserEmail);

            if (user is null || user.isRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }
            if (!await _roleManager.RoleExistsAsync(userToRole.RoleName))
            {
                throw new CustomException("Wrong role! Check the rolename", HttpStatusCode.BadRequest);
            }
            if(await _userManager.IsInRoleAsync(user,userToRole.RoleName))
            {
                throw new CustomException(ErrorConstant.ALREADY_IN_ROLE, HttpStatusCode.BadRequest);
            }

            await _userManager.AddToRoleAsync(user, userToRole.RoleName);
        }
        public async Task<UserModel> GetUserByIdAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }
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
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }

            var userList = _mapper.Map<IQueryable<User>, List<UserModel>>(usersQuery);

            return userList;
        }
        public async Task<List<UserModel>> GetUserListByRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);        
            if(!users.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }

            return _mapper.Map<IList<User>, List<UserModel>>(users);
        }
        public async Task RemoveUserAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }
            user.isRemoved = true;
            await _userManager.UpdateAsync(user);
            
        }
        public async Task UpdateUserAsync(UserUpdateModel userModel)
        {
            var checkUser = await _userManager.FindByIdAsync(userModel.Id.ToString());

            if (checkUser is null || checkUser.isRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }

            await _userManager.SetUserNameAsync(checkUser, userModel.Username);
            checkUser.FirstName = userModel.FirstName;
            checkUser.LastName = userModel.LastName;
   
            await _userManager.UpdateAsync(checkUser);    
        }

        public async Task BlockUser(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(100));
        }
        public async Task UnblockUser(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }
            await _userManager.SetLockoutEnabledAsync(user, false);       
        }

    }
}

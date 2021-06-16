using AutoMapper;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
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
                throw new CustomException(ErrorConstant.INCORRECT_ROLE, HttpStatusCode.BadRequest);
            }
            if(await _userManager.IsInRoleAsync(user,userToRole.RoleName))
            {
                throw new CustomException(ErrorConstant.ALREADY_IN_ROLE, HttpStatusCode.BadRequest);
            }

            await _userManager.AddToRoleAsync(user, userToRole.RoleName);
        }
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email.ToString());
            if (user is null)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }

            var userModel = _mapper.Map<UserModel>(user);
            return userModel;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var userList = await _userManager.Users.Where(u => !u.isRemoved).ToListAsync();
           
            return _mapper.Map<List<UserModel>>(userList);  
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

            var userList = _mapper.Map<List<UserModel>>(usersQuery);

            return userList;
        }
        public async Task<List<UserModel>> GetUserListByRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);        
            if(!users.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }
            
            return _mapper.Map<List<UserModel>>(users);
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
            var tokenHandler = new JwtSecurityTokenHandler();
            var userToken = tokenHandler.ReadJwtToken(userModel.Jwt);
            var userId = userToken.Claims.First(claim => claim.Type == "id").Value;

            var checkUser = await _userManager.FindByIdAsync(userId);

            if (checkUser is null || checkUser.isRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }

            await _userManager.SetUserNameAsync(checkUser, userModel.Username);
            checkUser.FirstName = userModel.FirstName;
            checkUser.LastName = userModel.LastName;
   
            await _userManager.UpdateAsync(checkUser);    
        }

       

    }
}

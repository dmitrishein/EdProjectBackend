using AutoMapper;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        IUserService _userService;

        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task AddToRole(UserToRoleModel userToRole)
        {
            await _userService.AddToRoleAsync(userToRole);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateUser(UserUpdateModel userUpdModel)
        {
            await _userService.UpdateUserAsync(userUpdModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveUser(long userId)
        {
            await _userService.RemoveUserAsync(userId);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<List<UserModel>> GetUserByRole(string roleName)
        {
            return await _userService.GetUserListByRole(roleName);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<List<UserModel>> GetAllUsers()
        {
            return _userService.GetAllUsersAsync();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public List<UserModel> GetUserByQuery(string searchString)
        {
            return _userService.GetUsersByQuery(searchString);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task BlockUser(long userId)
        {
            await _userService.BlockUser(userId);
        }


        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UnblockUser(long userId)
        {
            await _userService.UnblockUser(userId);
        }
    }
}

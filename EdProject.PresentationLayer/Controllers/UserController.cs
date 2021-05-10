using AutoMapper;
using EdProject.BLL;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
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
        #region Private Members
        IUserService _userService;
        IMapper _mapper;
       
        #endregion

        #region Constructor
        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #endregion

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task CreateUser(UserCreateModel createUserModel)
        {

            await _userService.CreateUserAsync(createUserModel);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task GetUser(long id)
        {
            await _userService.GetUserAsync(id);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateUser(UserUpdateModel userUpdViewModel)
        {
            await _userService.UpdateUserAsync(userUpdViewModel);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveUser(long userId)
        {
            await _userService.RemoveUserAsync(userId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<IList<User>> GetUserByRole(string roleName)
        {
            return await _userService.GetUserListByRole(roleName);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<List<User>> GetAllUsers()
        {
            return _userService.GetAllUsersAsync();
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

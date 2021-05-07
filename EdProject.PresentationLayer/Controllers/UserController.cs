using AutoMapper;
using EdProject.BLL;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Middleware;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task CreateUser(UserCreateViewModel createUserModel)
        {
            var createdUser = _mapper.Map<UserCreateViewModel, UserCreateModel>(createUserModel);

            await _userService.CreateUserAsync(createdUser);
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
        public async Task UpdateUser(UserUpdViewModel userUpdViewModel)
        {
            await _userService.UpdateUserAsync(_mapper.Map<UserUpdViewModel, UserUpdateModel>(userUpdViewModel));
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
        public async Task<IList<AppUser>> GetUserByRole(string roleName)
        {
            return await _userService.GetUserListByRole(roleName);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<List<AppUser>> GetAllUsers()
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

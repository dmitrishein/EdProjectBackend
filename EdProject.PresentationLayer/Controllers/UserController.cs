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
       
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }
        #endregion

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task CreateUser(UserCreateViewModel createUserModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserCreateViewModel, UserCreateModel>());
            var _mapper = new Mapper(config);
            var createdUser = _mapper.Map<UserCreateViewModel, UserCreateModel>(createUserModel);
            try
            {
                await _userService.CreateUserAsync(createdUser);
            }
            catch(Exception x)
            {
                throw new CustomException($"Cannot create user : {x.Message}",400);
            }
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserUpdViewModel, UserUpdateModel>());
            var _mapper = new Mapper(config);
            var updatedUser = _mapper.Map<UserUpdViewModel, UserUpdateModel>(userUpdViewModel);

            await _userService.UpdateUserAsync(updatedUser);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveUser(long userId)
        {
            try
            {
                await _userService.RemoveUserAsync(userId);
            }
            catch(Exception x)
            {
                throw new CustomException($"Can't remove user {x.Message}",400);
            }
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
        public IQueryable GetAllUsers()
        {
            return _userService.GetAllUsers();
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

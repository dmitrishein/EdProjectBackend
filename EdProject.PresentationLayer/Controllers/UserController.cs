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

        private readonly IUserService _userService;

        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task UpdateUser(UserUpdateModel userUpdModel)
        {
            await _userService.UpdateUserAsync(userUpdModel);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<UserModel> GetUserByEmail(string searchString)
        {
            return await _userService.GetUserByEmailAsync(searchString);
        }
    }
}

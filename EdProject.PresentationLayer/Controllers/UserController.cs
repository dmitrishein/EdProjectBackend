using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("[action]")]
        public async Task<AppUser> GetUser(long id)
        {
            var res = await _userService.GetUserAsync(id);

            return res;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public  Task<AppUser> UpdateUser(long id)
        {
            //var res = await _userService.UpdateUserAsync();
            return null;
            //return res;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<IList<AppUser>> GetUserByRole(string roleName)
        {
            var res = await _userService.GetUserListByRole(roleName);

            return res;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public IQueryable GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task BlockUser(string userId)
        {
            await _userService.BlockUser(userId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UnblockUser(string userId)
        {
            await _userService.UnblockUser(userId);
        }
    }
}

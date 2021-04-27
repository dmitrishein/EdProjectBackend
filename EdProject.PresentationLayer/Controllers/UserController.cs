using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
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


        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<AppUser> GetUserByEmail(string email)
        {
            var res = await _userService.GetUserByEmail(email);

            return res;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<AppUser> GetUserByUsername(string name)
        {
            var res = await _userService.GetUserByUsername(name);

            return res;
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

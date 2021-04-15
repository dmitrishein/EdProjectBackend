using EdProject.BLL.Services;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        #region Private Members
        UserService _userService;
        UserManager<AppUser> _userManager;
        RoleManager<AppRole> _roleManager;
        AppDbContext _dbContext;
        IConfiguration _config;
        #endregion

        #region Constructor
        public UserController(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = new UserService(_userManager,_roleManager);
            _config = config;
        }
        #endregion

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public IQueryable GetAdmins()
        {
            return _userService.GetAllUsers();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public IQueryable GetUsers()
        {
            return _userService.GetAllUsers();
        }

        //[Authorize(Roles = "admin")]
        //[HttpGet("[action]")]
        //public IQueryable GetUserByRole(string roleName)
        //{
        //    return (IQueryable)_userService.GetUserListByRole(roleName);
        //}

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
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(100));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UnblockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.SetLockoutEnabledAsync(user, false);
            }
        }
    }
}

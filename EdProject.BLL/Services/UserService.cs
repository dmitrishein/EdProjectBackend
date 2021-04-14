using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class UserService : IUserService
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AddToRoleAsync(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user != null && await _roleManager.RoleExistsAsync(role))
            {
               await _userManager.AddToRoleAsync(user, role);
            }
        }
        public async Task UpdateUserAsync(UserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.UserName);
            
            if(user != null)
            {
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
            }
        }


    }
}

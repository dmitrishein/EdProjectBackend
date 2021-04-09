using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<AppUser> _roleManager;

        public UserRepository(AppDbContext appDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppUser> roleManager) : base(appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task AddUserToRole(string email, string role)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);

            if(appUser != null)
            {
                await _userManager.AddToRoleAsync(appUser, role);
            }

        }
    }
}

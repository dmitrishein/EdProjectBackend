using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<AppRole> _roleManager;

        public UserRepository(AppDbContext appDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task CreateUserRole(string email, string role)
        {
           if(await _roleManager.RoleExistsAsync(role))
           {
              await _roleManager.CreateAsync(new AppRole(role));
           }
        }
        public async Task <bool> Authentication(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                if (await _userManager.IsEmailConfirmedAsync(user) && await _userManager.IsPhoneNumberConfirmedAsync(user))
                {
                    return true;
                }
            }

            return false;

        }
        public async Task AddUserToRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                if (await _roleManager.FindByNameAsync(role) != null)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}

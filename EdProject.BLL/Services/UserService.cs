using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class UserService : IUserService
    {
        #region UserManager and constructor
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
        }

        #endregion

        public async Task UserEdit(string userId, string userName, string firstName, string lastName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.UserName = userName;
            user.FirstName = firstName;
            user.LastName = lastName;

             await _userManager.UpdateAsync(user);  
        }
    }
}

using EdProject.DAL.Entities;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    interface IUserService
    {
        Task AddToRoleAsync(string userId, string role);
        Task UpdateUserAsync(UserModel userModel);
        Task RemoveUserAsync(string userName);
        Task CreateUserAsync(UserModel userModel);
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> GetUserByEmail(string email);

    }
}

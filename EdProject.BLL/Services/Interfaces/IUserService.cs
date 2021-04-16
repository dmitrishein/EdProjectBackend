using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task AddToRoleAsync(string userId, string role);
        Task UpdateUserAsync(UserModel userModel);
        Task RemoveUserAsync(string userName);
        Task CreateUserAsync(UserModel userModel);
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> GetUserByEmail(string email);
        public IQueryable GetAllUsersByQuery(string searchString);
        public IQueryable GetAllUsers();
        public Task<IList<AppUser>> GetUserListByRole(string roleName);
        public Task BlockUser(string userId);
        public Task UnblockUser(string userId);


    }
}

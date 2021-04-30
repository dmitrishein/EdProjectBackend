using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddToRoleAsync(string userId, string role);
        public Task UpdateUserAsync(UserUpdateModel userModel);
        public Task RemoveUserAsync(long userId);
        public Task CreateUserAsync(UserCreateModel userModel);
        public Task<AppUser> GetUserAsync(long userId);
        public IQueryable GetAllUsersByQuery(string searchString);
        public IQueryable GetAllUsers();
        public Task<IList<AppUser>> GetUserListByRole(string roleName);
        public Task BlockUser(long userId);
        public Task UnblockUser(long userId);


    }
}

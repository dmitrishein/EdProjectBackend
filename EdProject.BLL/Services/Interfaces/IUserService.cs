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
        public Task<User> GetUserAsync(long userId);
        public Task<List<User>> GetAllUsersByQuery(string searchString);
        public Task<List<User>> GetAllUsersAsync();
        public Task<IList<User>> GetUserListByRole(string roleName);
        public Task BlockUser(long userId);
        public Task UnblockUser(long userId);


    }
}

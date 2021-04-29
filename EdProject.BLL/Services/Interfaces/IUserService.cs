using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddToRoleAsync(string userId, string role);
        public Task UpdateUserAsync(UserRegistrationModel userModel);
        public Task RemoveUserAsync(string userName);
        public Task CreateUserAsync(UserRegistrationModel userModel);
        public Task<AppUser> GetUserAsync(long userId);
        public IQueryable GetAllUsersByQuery(string searchString);
        public IQueryable GetAllUsers();
        public Task<IList<AppUser>> GetUserListByRole(string roleName);
        public Task BlockUser(string userId);
        public Task UnblockUser(string userId);


    }
}

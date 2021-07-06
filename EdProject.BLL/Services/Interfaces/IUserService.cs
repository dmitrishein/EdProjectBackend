using EdProject.BLL.Models.User;
using EdProject.BLL.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddToRoleAsync(UserToRoleModel userToRole);
        public Task UpdateUserAsync(UserUpdateModel userModel);
        public Task RemoveUserAsync(long userId);
        public Task<ProfileViewModel> UserProfileViewModel(string username);
        public Task<UserModel> GetUserByEmailAsync(string email);
        public List<UserModel> GetUsersByQuery(string searchString);
        public Task<List<UserModel>> GetAllUsersAsync();
        public Task<List<UserModel>> GetUserListByRole(string roleName);

    }
}

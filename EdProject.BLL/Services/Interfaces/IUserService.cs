﻿using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddToRoleAsync(UserToRoleModel userToRole);
        public Task UpdateUserAsync(UserUpdateModel userModel);
        public Task RemoveUserAsync(long userId);
        public Task<UserModel> GetUserByIdAsync(long userId);
        public List<UserModel> GetUsersByQuery(string searchString);
        public Task<List<UserModel>> GetAllUsersAsync();
        public Task<List<UserModel>> GetUserListByRole(string roleName);
        public Task BlockUser(long userId);
        public Task UnblockUser(long userId);


    }
}

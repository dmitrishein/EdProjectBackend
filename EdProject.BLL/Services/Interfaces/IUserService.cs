using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    interface IUserService
    {
        Task AddToRoleAsync(string userId, string role);
        Task UpdateUserAsync(UserModel userModel);
        
    }
}

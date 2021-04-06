using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AppUser> RegisterUser(string userName, string firstName, string lastName, string password, string email);
        Task<Boolean> Login(string password, string email); 
        Task LogOff(string password, string email);
        Task<bool> ConfirmEmail(string userId, string token);
    }
}

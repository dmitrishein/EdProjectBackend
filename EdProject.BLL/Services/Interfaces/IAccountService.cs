using EdProject.DAL.Entities;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUser(UserModel newUser);
        Task<bool> Login(string password, string email, bool RememberMe); 
        Task Logout(string password, string email);
        Task<bool> ConfirmEmail(string userId, string token);
    }
}

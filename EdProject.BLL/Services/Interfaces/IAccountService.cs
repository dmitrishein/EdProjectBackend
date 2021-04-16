using EdProject.DAL.Entities;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> RegisterUserAsync(UserModel newUser);
        Task<bool> SignInAsync(string password, string email, bool RememberMe); 
        Task SignOutAsync(string password, string email);
        Task<bool> ConfirmEmailAsync(string token, string email);
        Task<string> PasswordRecoveryAsync(string email);
        public Task<bool> ResetPasswordAsync(string token, string email, string newPasssword);
        public Task SendEmail(string confirmationLink, string email, string subject);


    }
}

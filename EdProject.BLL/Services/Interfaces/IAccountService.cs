using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<string>> RegisterUserAsync(UserRegistrationModel newUser);
        Task<bool> SignInAsync(UserSignInModel userSignInModel); 
        Task SignOutAsync();
        Task<bool> ConfirmEmailAsync(string token, string email);
        Task<string> PasswordRecoveryAsync(string email);
        public Task<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
        public Task SendEmail(EmailConfirmationModel emailModel);
        public Task<AppUser> GetUserByEmailAsync(string email);
        public Task<IList<string>> GetUserRoleAsync(string email);


    }
}

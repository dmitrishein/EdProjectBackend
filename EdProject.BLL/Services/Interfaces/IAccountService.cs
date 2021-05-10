using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserCreateModel newUser);
        Task SignInAsync(LoginModel userSignInModel); 
        Task SignOutAsync();
        Task ConfirmEmailAsync(string token, string email);
        Task<string> PasswordRecoveryTokenAsync(string email);
        public Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
        public Task SendEmail(EmailModel emailModel);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<IList<string>> GetUserRoleAsync(string email);


    }
}

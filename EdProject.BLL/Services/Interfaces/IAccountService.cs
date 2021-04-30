using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserCreateModel newUser);
        Task SignInAsync(UserSignInModel userSignInModel); 
        Task SignOutAsync();
        Task ConfirmEmailAsync(string token, string email);
        Task<string> PasswordRecoveryTokenAsync(string email);
        public Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
        public Task SendEmail(EmailConfirmationModel emailModel);
        public Task<AppUser> GetUserByEmailAsync(string email);
        public Task<IList<string>> GetUserRoleAsync(string email);


    }
}

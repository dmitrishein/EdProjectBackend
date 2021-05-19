using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserCreateModel newUser);
        Task<TokenPairModel> SignInAsync(LoginModel userSignInModel); 
        Task SignOutAsync();
        Task ConfirmEmailAsync(EmailValidationModel validationModel);
        Task ResetPasswordTokenAsync(string email);
        public Task ChangePasswordAsync(ChangePasswordModel resetPasswordModel);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<IList<string>> GetUserRoleAsync(string email);


    }
}

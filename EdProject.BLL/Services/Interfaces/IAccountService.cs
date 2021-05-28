using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegistrationModel newUser);
        Task<TokenPairModel> SignInAsync(LoginModel userSignInModel); 
        Task SignOutAsync();
        Task ConfirmEmailAsync(EmailValidationModel validationModel);
        Task ResetPasswordTokenAsync(string email);
        public Task ChangePasswordAsync(ChangePasswordModel resetPasswordModel);
    }
}

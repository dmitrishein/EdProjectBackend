using EdProject.BLL;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
      

        [HttpPost("[action]")]
        public async Task Registration(UserCreateModel register)
        {        
            await _accountService.RegisterUserAsync(register);
        }

        [HttpPost("[action]")]
        public async Task ConfirmEmail(EmailValidationModel validationModel)
        {
           await _accountService.ConfirmEmailAsync(validationModel);
        }

        [HttpPost("[action]")]
        public async Task<TokenPairModel> Login(LoginModel login)
        {
           return await _accountService.SignInAsync(login);
        }

        [HttpPost("[action]")]
        public async Task Logout()
        {
            await _accountService.SignOutAsync();
        }

        [HttpPost("[action]")]
        public async Task ResetPassword(string email)
        {
            await _accountService.ResetPasswordTokenAsync(email);
        }

        [HttpPost("[action]")]
        public async Task ChangePassword(ChangePasswordModel resetPasswordModel)
        {
            await _accountService.ChangePasswordAsync(resetPasswordModel);
        }

    }
}

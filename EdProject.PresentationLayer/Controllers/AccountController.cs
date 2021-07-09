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
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("[action]")]
        public async Task Registration(RegistrationModel register)
        {
            await _accountService.RegisterUserAsync(register);
        }

        [HttpPost("[action]")]
        public async Task ConfirmEmail(EmailValidationModel validationModel)
        {
            await _accountService.ConfirmEmailAsync(validationModel);
        }

        [HttpPost("[action]")]
        public async Task<TokenPairModel> Login(LoginDTOModel login)
        {
            return await _accountService.SignInAsync(login);
        }

        [HttpPost("[action]")]
        public async Task<TokenPairModel> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            return await _accountService.RefreshTokensAsync(refreshTokenModel.RefreshToken);
        }

        [HttpPost("[action]")]
        public async Task Logout()
        {
            await _accountService.SignOutAsync();
        }

        [HttpPost("[action]")]
        public async Task ResetPassword(ChangePasswordModel email)
        {
            await _accountService.ResetPasswordTokenAsync(email.Email);
        }
        [HttpPost("[action]")]
        public async Task ChangePassword(ChangePasswordModel resetPasswordModel)
        {
            await _accountService.ChangePasswordAsync(resetPasswordModel);
        }

    }
}

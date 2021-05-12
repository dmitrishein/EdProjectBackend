using EdProject.BLL;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            var emailValidationToken = await _accountService.RegisterUserAsync(register);

            EmailModel emailConfirmationModel = new()
            {
                RecipientName = register.FirstName,
                Email = register.Email,
                Message = Url.Action("ChangePassword", "Account", new {token = emailValidationToken, email = register.Email}, Request.Scheme),
                Subject = "Account Confirmation"
            };

            await _accountService.SendEmail(emailConfirmationModel);
        }

        [HttpPost("[action]")]
        public async Task ConfirmEmail(EmailValidationModel validationModel)
        {
           await _accountService.ConfirmEmailAsync(validationModel);
        }

        [HttpPost("[action]")]
        public async Task<TokenPairModel>Login(LoginModel login)
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
            var resetToken = await _accountService.ResetPasswordTokenAsync(email);
            EmailModel emailMessage = new()
            {
                    Email = email,
                    Message = $"https://localhost:44386/Account/ChangePassword?token={resetToken}&email={email}",
                    Subject = "Reset Password"
            };
            await _accountService.SendEmail(emailMessage);
        }

        [HttpPost("[action]")]
        public async Task ChangePassword(ChangePasswordModel resetPasswordModel)
        {
            await _accountService.ChangePasswordAsync(resetPasswordModel);
        }

    }
}

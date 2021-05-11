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
        IConfiguration _config;

        public AccountController(IAccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
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
        public async Task Login(LoginModel login)
        {
            JwtProvider jwt = new JwtProvider(_config);
            await _accountService.SignInAsync(login);
            var tokenString = await jwt.GenerateAccessToken(await _accountService.GetUserByEmailAsync(login.Email), _accountService);
            var refreshTokenString = jwt.GenerateRefreshToken(); 
        }

        [HttpPost("[action]")]
        public async Task Logout()
        {
            await _accountService.SignOutAsync();
        }

        [HttpPost("[action]")]
        public async Task ResetPassword(string email)
        {
            var recoveryToken = await _accountService.ResetPasswordTokenAsync(email);
            EmailModel emailMessage = new()
            {
                    Email = email,
                    Message = $"https://localhost:44386/Account/ChangePassword?token={recoveryToken}&email={email}",
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

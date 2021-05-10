using AutoMapper;
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
        IMapper _mapper;

        public AccountController(IAccountService accountService, IConfiguration config, IMapper mapper)
        {
            _accountService = accountService;
            _config = config;
            _mapper = mapper;
        }
      

        [HttpPost("[action]")]
        public async Task Registration(UserCreateModel register)
        {        
            await _accountService.RegisterUserAsync(register);
        }

        [HttpPost("[action]")]
        public async Task ConfirmEmail(string token , string email)
        {
           await _accountService.ConfirmEmailAsync(token, email);
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
        public async Task ForgotPassword(string email)
        {
            var recoveryToken = await _accountService.PasswordRecoveryTokenAsync(email);
            var confirmationLink = Url.Action("ResetPassword", "Account", new { token = recoveryToken, email = email }, Request.Scheme);
            EmailModel emailConfirmModel = new()
                {
                    Email = email,
                    Message = confirmationLink,
                    Subject = "Reset Password"
                };

            await _accountService.SendEmail(emailConfirmModel);
        }

        [HttpPost("[action]")]
        public async Task ResetPassword(string token, string email)
        {
            ResetPasswordModel resetModel = new ResetPasswordModel
            {
                Email = email,
                Token = token
            };
            await _accountService.ResetPasswordAsync(resetModel);
        }

    }
}

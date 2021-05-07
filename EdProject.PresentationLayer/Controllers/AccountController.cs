using AutoMapper;
using EdProject.BLL;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.PresentationLayer.Helpers;
using EdProject.PresentationLayer.Middleware;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public async Task Registration(RegisterViewModel register)
        {        
            var newUser = _mapper.Map<RegisterViewModel,UserCreateModel>(register);
            await _accountService.RegisterUserAsync(newUser);
        }

        [HttpPost("[action]")]
        public async Task ConfirmEmail(string token , string email)
        {
           await _accountService.ConfirmEmailAsync(token, email);
        }

        [HttpPost("[action]")]
        public async Task Login(LoginViewModel login)
        {
            JwtProvider jwt = new JwtProvider(_config);
            
            await _accountService.SignInAsync(_mapper.Map<LoginViewModel,UserSignInModel>(login));
            var tokenString = await jwt.GenerateAccessToken(await _accountService.GetUserByEmailAsync(login.Email), _accountService);
            var refreshTokenString = jwt.GenerateRefreshToken(); 
        }

        [HttpPost("[action]")]
        public async Task ForgotPassword(string email)
        {
            var recoveryToken = await _accountService.PasswordRecoveryTokenAsync(email);
            var confirmationLink = Url.Action("ResetPassword", "Account", new { token = recoveryToken, email = email }, Request.Scheme);
            EmailConfirmationModel emailConfirmModel = new()
                {
                    Email = email,
                    ConfirmationLink = confirmationLink,
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

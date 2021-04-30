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

        public AccountController(IAccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
        }
      

        [HttpPost("[action]")]
        public async Task Registration(RegisterViewModel register)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, BLL.UserCreateModel>());
            var _mapper = new Mapper(config);
            var newUser = _mapper.Map<RegisterViewModel, BLL.UserCreateModel>(register);
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
           
            UserSignInModel userSignInModel = new()
            {
                Email = login.Email,
                Password = login.Password,
                RememberMe = login.RememberMe
            };

            try
            {
                await _accountService.SignInAsync(userSignInModel);
                var user = await _accountService.GetUserByEmailAsync(login.Email);
                var tokenString = await jwt.GenerateAccessToken(user, _accountService);
                var refreshTokenString = jwt.GenerateRefreshToken();
            }
            catch (CustomException x)
            {
                throw new CustomException($"Failed to login. {x.Message}",400);
            }

        }

        [HttpPost("[action]")]
        public async Task ForgotPassword(string email)
        {
            try
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
            catch(CustomException x)
            {
                throw new CustomException($"Failed to recovery password {x.Message}",400);
            }
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

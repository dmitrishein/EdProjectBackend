using AutoMapper;
using EdProject.BLL;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.PresentationLayer.Helpers;
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
        public async Task<IActionResult> Registration([FromBody] RegisterViewModel register)
        {
            //UserRegistrationModel userModel = new UserRegistrationModel 
            //{
            //    UserName = register.Username,
            //    FirstName = register.FirstName,
            //    LastName = register.LastName,
            //    Password = register.Password, 
            //    Email = register.Email
            //};
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, UserRegistrationModel>());
            var _mapper = new Mapper(config);
            var newUser = _mapper.Map<RegisterViewModel, UserRegistrationModel>(register);

            var res =  await _accountService.RegisterUserAsync(newUser);
            return StatusCode(200);
        }

        [HttpPost("[action]")]
        public async Task ConfirmEmail(string token , string email)
        {
           await _accountService.ConfirmEmailAsync(token, email);
        }

        [HttpPost("[action]")]
        public async Task<OkObjectResult> Login(LoginViewModel login)
        {
            JwtProvider jwt = new JwtProvider(_config);
            var tokenString="";
            var refreshTokenString = "";
            UserSignInModel userSignInModel = new()
            {
                Email = login.Email,
                Password = login.Password,
                RememberMe = login.RememberMe
            };

            if (await _accountService.SignInAsync(userSignInModel))
            {
                 var user = await _accountService.GetUserByEmailAsync(login.Email);
                 tokenString = await jwt.GenerateAccessToken(user,_accountService);
                 refreshTokenString = jwt.GenerateRefreshToken();
            }

            return Ok(new { access_token = tokenString, refresh_token = refreshTokenString});
        }




        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("[action]")]
        public async Task ForgotPassword(string email)
        {
            var recoveryToken = await _accountService.PasswordRecoveryAsync(email);
            var confirmationLink = Url.Action("ResetPassword", "Account", new { token = recoveryToken, email = email }, Request.Scheme);
            EmailConfirmationModel emailConfirmModel = new()
            {
                Email= email,
                ConfirmationLink = confirmationLink,
                Subject = "Reset Password"
            };
            await _accountService.SendEmail(emailConfirmModel);
        }

        [HttpPost("[action]")]
        public RedirectToActionResult ResetPassword(string token, string email)
        {
            ResetPasswordModel resModel = new ResetPasswordModel
            {
                Email = email,
                Token = token
            };

            return RedirectToAction("PasswordUpdate","Account",resModel);
        }

        [HttpPost("[action]")]
        public async Task PasswordUpdate(ResetPasswordModel resetModel)
        {   
            await _accountService.ResetPasswordAsync(resetModel);
        }

    }
}

using EdProject.BLL;
using EdProject.BLL.Services;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Helpers;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        #region Private Members
        AccountService _accountService;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        AppDbContext _dbContext;
        IConfiguration _config;
        #endregion

        public AccountController(AppDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = new AccountService(_userManager, _signInManager);
            _config = config;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("[action]")]
        public async Task Registration(RegisterViewModel register)
        {
            UserModel userModel = new UserModel {UserName = register.UserName, FirstName = register.FirstName, LastName = register.LastName, Password = register.Password, Email = register.Email};
            var token =  await _accountService.RegisterUserAsync(userModel);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token = token, email = userModel.Email }, Request.Scheme);
            await _accountService.SendEmail(confirmationLink, userModel.Email,"Confirm Account");
        }

        
        [HttpGet]
        [Route("[action]")]
        public async Task ConfirmEmail(string token , string email)
        {
           await _accountService.ConfirmEmailAsync(token, email);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<OkObjectResult> Login(LoginViewModel login)
        {

            JwtHelper jwt = new JwtHelper(_config);
            var tokenString="";
            var refreshTokenString = "";
            if (await _accountService.SignInAsync(login.Password, login.Email, false))
            {
                 tokenString = jwt.GenerateAccessToken(login);
                 refreshTokenString = jwt.GenerateRefreshToken();
            }

            return Ok(new { access_token = tokenString, refresh_token = refreshTokenString});
        }

        [HttpPost("[action]")]
        public async Task ForgotPassword(string email)
        {
            var recoveryToken = await _accountService.PasswordRecoveryAsync(email);
            var confirmationLink = Url.Action("ResetPassword", "Account", new { token = recoveryToken, email = email }, Request.Scheme);
            await _accountService.SendEmail(confirmationLink, email, "Reset Password");
        }

        [HttpPost("[action]")]
        public async Task ResetPassword(string token,string email, ResetPasswordModel resetPasswordModel)
        {
            await _accountService.ResetPassword(token,email,resetPasswordModel.Password);
        }


        


    }
}

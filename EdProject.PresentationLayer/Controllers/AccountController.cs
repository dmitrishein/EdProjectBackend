using EdProject.BLL;
using EdProject.BLL.Services;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        #region Private Members
        AccountService _accountService;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        AppDbContext _dbContext;
        #endregion

        public AccountController(AppDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = new AccountService(_userManager, _signInManager);
        }


        [Route("Registration")]
        [HttpGet]
        public async Task Registration(RegisterViewModel register)
        {
            UserModel userModel = new UserModel {UserName = register.UserName, FirstName = register.FirstName, LastName = register.LastName, Password = register.Password, Email = register.Email};
            
            var token =  await _accountService.RegisterUser(userModel);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token = token, email = userModel.Email }, Request.Scheme);
            await _accountService.SendEmailConfiramtion(confirmationLink, userModel.Email);
        }

        [Route("ConfirmEmail")]
        [HttpGet]
        public async Task ConfirmEmail(string token , string email)
        {
           await _accountService.ConfirmEmail(token, email);
        }

        [Route("Login")]
        [HttpGet]
        public async Task Login(LoginViewModel login)
        {
           await _accountService.Login(login.Password, login.Email, false);
        }
    }
}

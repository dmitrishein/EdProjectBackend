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

        private AccountService _accountService;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private AppDbContext _dbContext;
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
            
            await _accountService.RegisterUser(userModel);

        }
    }
}

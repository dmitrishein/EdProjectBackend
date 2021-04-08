using EdProject.BLL;
using EdProject.BLL.Services;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService accountService;
      
        [Route("Registration")]
        [HttpGet]
        public async Task Registration(string username, string email, string firstName, string lastName, string password)
        {
            UserModel userModel = new UserModel { UserName = username, FirstName = firstName, LastName = lastName, Password = password, Email = email};
            
            await accountService.RegisterUser(userModel);

        }
    }
}

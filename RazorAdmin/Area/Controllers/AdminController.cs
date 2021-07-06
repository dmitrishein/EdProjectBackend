using Microsoft.AspNetCore.Mvc;

namespace RazorAdmin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public string LoginAction()
        {
            return "Login";
        }
    }
}

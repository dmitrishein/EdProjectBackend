using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        public AdminController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            Console.WriteLine("sosi");
            return View();
        }
    }
}

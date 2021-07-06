using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Areas.Admin.Controllers
{

    [Authorize(AuthenticationSchemes = AuthSchemes, Roles = "admin")]
    public class AdminController : Controller
    {
        private const string AuthSchemes = CookieAuthenticationDefaults.AuthenticationScheme + ","
            + JwtBearerDefaults.AuthenticationScheme;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IAuthorService _authorService;
        public AdminController(IAccountService accountService,IUserService userService,IAuthorService authorService)
        {
            _accountService = accountService;
            _userService = userService;
            _authorService = authorService;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDTOModel login)
        {
            var tokens = await _accountService.SignInAsync(login);
            var jwtToken = new JwtSecurityToken(tokens.AccessToken);
            var claims = jwtToken.Claims.Where(x => x.Type.EndsWith("role") || x.Type.EndsWith("email"));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationType: "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var emailFromClaim = User.Claims.Where(x => x.Type.EndsWith("email")).FirstOrDefault().Value;
            var userModel = await _userService.UserProfileViewModel(emailFromClaim);
            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var newModel = await _authorService.GetAuthorsViewModel();
            return View(newModel);
        }
    }
}

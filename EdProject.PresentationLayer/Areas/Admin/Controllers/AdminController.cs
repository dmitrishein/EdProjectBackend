using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.AuthorDTO;
using EdProject.BLL.Models.Editions;
using EdProject.BLL.Models.User;
using EdProject.BLL.Models.ViewModels;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Models;
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
        private readonly IEditionService _editionService;
        public AdminController(IAccountService accountService, IUserService userService, IAuthorService authorService, IEditionService editionService)
        {
            _accountService = accountService;
            _userService = userService;
            _authorService = authorService;
            _editionService = editionService;
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDTOModel login)
        {
            var tokens = await _accountService.SignInAsync(login);
            var jwtToken = new JwtSecurityToken(tokens.AccessToken);
            var claims = jwtToken.Claims.Where(x => x.Type.EndsWith("role") || x.Type.EndsWith("email") || x.Type.EndsWith("id"));

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
            var id = User.Claims.Where(x => x.Type.EndsWith("id")).FirstOrDefault().Value;
            var userModel = await _userService.UserProfileViewModel(id);
            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> Authors()
        {
            var newModel = await _authorService.GetAuthorsViewModel();
            return View(newModel);
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var newModel = await _userService.UsersViewModel();
            return View(newModel);
        }

        [HttpGet]
        public async Task<IActionResult> Products(int sortBy = 0, int curent = 1, bool isDesending = false )
        {
            EditionPageParameters filter = new EditionPageParameters
            {
                ElementsPerPage = 5,
                CurrentPageNumber = curent,
                SearchString = "",
                MaxUserPrice = 0,
                MinUserPrice = 0,
                EditionTypes = new EditionTypes[]
                {
                    EditionTypes.Book,
                    EditionTypes.Magazine,
                    EditionTypes.Newspaper,
                    EditionTypes.None
                },
                SortType = (DAL.Enums.EditionSortingType)sortBy,
                IsReversed = isDesending
            }; 
            var newModel = await _editionService.GetViewModelAsync(filter);
      
            return View(newModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromForm] UserModel user)
        {
            await _userService.UpdateUserAsync(user, true);
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm] ProfileViewModel viewModel)
        {
            await _accountService.AdminChangePasswordAsync(viewModel.User.Email, viewModel.NewPassword);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAuthor([FromForm] AuthorModel author)
        {
            await _authorService.CreateAuthorAsync(author);
            return RedirectToAction("Authors");
        }
        [HttpGet]
        public async Task<IActionResult> RemoveAuthor(long id)
        {
            await _authorService.RemoveAuthorAsync(id);
            return RedirectToAction("Authors");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateAuthor(long id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return View("UpdateAuthor",author);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAuthor([FromForm] AuthorModel author)
        {
            await _authorService.UpdateAuthorAsync(author);
            return RedirectToAction("Authors");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddAuthorToEdition([FromBody]AddAuthorToEdition edition)
        {
            await _authorService.AddAuthorToEditionAsync(edition);
            return RedirectToAction("Products");
        }

        [HttpGet]
        public IActionResult AddEdition()
        {
              return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEdition([FromForm] CreateEditionModel edition)
        {
            await _editionService.CreateEditionAsync(edition);
            return RedirectToAction("Products");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveEdition(long id)
        {
            await _editionService.RemoveEditionAsync(id);
            return RedirectToAction("Products");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEdition(long id)
        {
            var author = await _editionService.GetEditionByIdAsync(id);
            return View("UpdateEdition", author);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEdition([FromForm] CreateEditionModel editionModel)
        {
            await _editionService.UpdateEditionAsync(editionModel);
            return RedirectToAction("Products");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateClient(long id)
        {
            var client = await _userService.GetUserByIdAsync(id);
            return View("UpdateClient", client);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient([FromForm] UserModel editionModel)
        {
            await _userService.UpdateUserAsync(editionModel, true);
            return RedirectToAction("Users");
        }
        [HttpGet]
        public async Task<IActionResult> RemoveClient(long id)
        {
            await _userService.RemoveUserAsync(id);
            return RedirectToAction("Users");
        }


    }
}

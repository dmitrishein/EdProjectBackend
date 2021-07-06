using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminLayer.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        public LoginDTOModel loginModel { get; set; }

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet()
        {
        }
        public async Task OnPost()
        {
            var tokens = await _accountService.SignInAsync(loginModel);
            await Authenticate(tokens);
        }

        public async Task Authenticate(TokenPairModel tokens)
        {
            var jwtToken = new JwtSecurityToken(tokens.AccessToken);
            var claims = jwtToken.Claims.Where(x => x.Type.EndsWith("role") || x.Type.EndsWith("email"));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationType: "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return;
        }
    }
}

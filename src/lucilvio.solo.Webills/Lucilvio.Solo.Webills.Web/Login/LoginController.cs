using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Lucilvio.Solo.Webills.Web.Login
{
    public class LoginController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var issuer = "http://localhost:5000";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Test User", issuer)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddYears(10)
            });

            return RedirectToAction("Index", "Home");
        } 
    }
}
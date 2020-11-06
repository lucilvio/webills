using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Login
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly Module _module;

        public LoginModel(Module module)
        {
            this._module = module;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/Home/Dashboard");

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(LoginRequest request, [FromServices] IHttpContextAccessor contextAcessor)
        {
            var loggedUser = await this._module.Login(request);

            var issuer = "webills.com";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loggedUser.Name, issuer),
                new Claim(ClaimTypes.Email, loggedUser.Email, issuer),
                new Claim(ClaimTypes.NameIdentifier, loggedUser.Id.ToString(), issuer)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "login");   
            await contextAcessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    IsPersistent = true,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(15),
                });

            return this.RedirectToPage("/Home/Dashboard");
        }

        public class LoginRequest : ILoginMessage
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
    }
}

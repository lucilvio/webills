using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Lucilvio.Solo.Webills.Web.Home;
using Lucilvio.Solo.Webills.UseCases.Contracts.Logon;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class LogonController : Controller
    {
        private readonly ILogon _login;
        private readonly ISecurityService _securityService;

        public LogonController(ILogon login, ISecurityService securityService)
        {
            this._login = login ?? throw new ArgumentNullException(nameof(login));
            this._securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LogonRequest request)
        {
            await this._login.Execute(new LogonCommandAdapter(request), async user =>
            {
                await this._securityService.Authenticate(new Credentials(user.Login.Value, user.Name));
            });

            return RedirectToAction(nameof(HomeController.Dashboard), "Home");
        }

        //public async Task Authenticate(LoggedUser user)
        //{
        //    var issuer = "http://localhost:5000";

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Name, issuer),
        //        new Claim(ClaimTypes.NameIdentifier, user.Login.Value, issuer)
        //    };

        //    var userIdentity = new ClaimsIdentity(claims, "login");

        //    var claimsPrincipal = new ClaimsPrincipal(userIdentity);

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
        //    {
        //        IsPersistent = true,
        //        AllowRefresh = true,
        //        ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
        //    }).ConfigureAwait(false);
        //}
    }
}
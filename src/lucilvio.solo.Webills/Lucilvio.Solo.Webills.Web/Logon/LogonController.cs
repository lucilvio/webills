using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Lucilvio.Solo.Webills.Web.Home;
using Lucilvio.Solo.Webills.UseCases.Contracts.Logon;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class LogonController : Controller
    {
        private readonly ILogon _login;
        private readonly IAuthentication _authenticationService;

        public LogonController(ILogon login, IAuthentication authenticationService)
        {
            this._login = login ?? throw new ArgumentNullException(nameof(login));
            this._authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
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
                await this._authenticationService.SignIn(new AuthCredentials(user.Id, user.Login, user.Name));
            });

            return RedirectToAction(nameof(HomeController.Dashboard), "Home");
        }
    }
}
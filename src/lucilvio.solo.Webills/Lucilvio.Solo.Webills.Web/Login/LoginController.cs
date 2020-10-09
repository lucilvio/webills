using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.Web.Home;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Module _module;

        public LoginController(IAuthenticationService authenticationService, Module module)
        {
            this._authenticationService = authenticationService;
            this._module = module;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            await this._module.SendMessage(Module.Messages.Login, request);
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        
    }
}
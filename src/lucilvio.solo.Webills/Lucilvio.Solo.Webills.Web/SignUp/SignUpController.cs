﻿using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Messages;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromServices] Module module, [FromForm] SignUpRequest request)
        {
            await module.SendMessage(Module.Messages.CreateAccount, request);

            this.SendSuccessMessage($"Welcome {request.Name}! You can make your login now.");

            return this.RedirectToAction(nameof(LoginController.Index), nameof(LoginController.Login));
        }
    }
}
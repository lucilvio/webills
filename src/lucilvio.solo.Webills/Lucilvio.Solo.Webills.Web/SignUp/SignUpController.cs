﻿using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.Web.Logon;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromServices]ICreateUserAccountUseCase createUserAccount, [FromForm]RegisterRequest request)
        {
            await createUserAccount.Execute(new CreateUserAccountCommandAdapter(request));

            return RedirectToAction(nameof(LogonController.Index), "Logon");
        }
    }
}

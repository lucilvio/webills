using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.Web.Home;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    public partial class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromServices]UserAccountModule userAccountModule, [FromForm]LoginRequest request)
        {
            try
            {
                var message = new LoginInput(request.Login, request.Password);
                await userAccountModule.SendMessage(message);

                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (Exception ex)
            {
                this.TempData["errorMessage"] = ex.Message;
                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
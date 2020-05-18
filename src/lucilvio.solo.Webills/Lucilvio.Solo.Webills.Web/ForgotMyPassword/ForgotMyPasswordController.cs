using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.ForgotMyPassword
{
    public class ForgotMyPasswordController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMeANewPassword([FromServices]UserAccountModule userAccountModule,
            [FromForm]SendMeANewPasswordRequest request)
        {
            var message = new GenerateNewPasswordInput(request.Email);
            await userAccountModule.SendMessage(message);

            return this.RedirectToAction("Index", "Login");
        }
    }
}
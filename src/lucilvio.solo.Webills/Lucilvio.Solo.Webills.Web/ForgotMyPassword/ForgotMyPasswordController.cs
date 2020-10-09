using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Messages;
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
        public async Task<IActionResult> SendMeANewPassword([FromServices]Module userAccountModule,
            [FromForm]SendMeANewPasswordRequest request)
        {
            await userAccountModule.SendMessage(Module.Messages.NewPassword, request);

            this.SendSuccessMessage($"Instructions to how you can get your password back were sent to the email {request.Email}");

            return this.RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}
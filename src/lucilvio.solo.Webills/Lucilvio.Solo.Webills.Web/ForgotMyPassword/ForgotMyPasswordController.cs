using System;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMeANewPassword(
            [FromServices]INotificationService notificationService,
            [FromServices]UserAccountModule userAccountModule,
            [FromForm]SendMeANewPasswordRequest request)
        {
            await userAccountModule.GenerateNewPassword(new SendNewPasswordInput(request.Email), async generatedPassword =>
            {
                await notificationService.Send(new Notification(
                    new Notification.Sender("Webillls", "webills@mail.com"),
                    new Notification.Receiver(generatedPassword.UserName, generatedPassword.UserContact),
                    @$"Hey <b>{generatedPassword.UserName}</b><br>Here is your new password: <b>{generatedPassword.Password}</b>"));
            });

            return RedirectToAction("Index", "Login");
        }
    }
}

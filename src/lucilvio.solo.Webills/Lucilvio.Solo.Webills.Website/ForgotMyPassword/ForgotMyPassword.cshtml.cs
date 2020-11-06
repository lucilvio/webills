using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.Website.Shared.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.ForgotMyPassword
{
    [AllowAnonymous]
    public class ForgotMyPasswordModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(ForgotMyPasswordRequest request, [FromServices] UserAccount.Module module,
            [FromServices] INotificationService notificationService)
        {
            var generatedPassword = await module.GenerateNewPassword(request);

            await notificationService.Send(new Notification(
                new Notification.Sender("Admin", "admin@webills.com"),
                new Notification.Receiver(generatedPassword.UserName, generatedPassword.UserContact),
                "WEBills - Did you forget you password?",
                $"Hey <b> {generatedPassword.UserName} </b>, how are you?<br /> Here is your wew Password: <b> {generatedPassword.Password} </b>"));

            return RedirectToPage("/Login/Login");
        }

        public class ForgotMyPasswordRequest : IGenerateNewPasswordMessage
        {
            public string Email { get; set; }
        }
    }
}
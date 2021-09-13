using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.Website.Shared;
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
            var message = new GenerateNewPasswordMessage(request.Email);
            await module.SendMessage(message);
 
            var generatedPassword = message.Response;

            await notificationService.Send(new Notification(
                new Notification.Sender("Admin", "admin@webills.com"),
                new Notification.Receiver(generatedPassword.UserName, generatedPassword.UserContact),
                "WEBills - Did you forget you password?",
                $"Hey <b> {generatedPassword.UserName} </b>, how are you?<br /> Here is your wew Password: <b> {generatedPassword.Password} </b>"));

            this.SendSuccessMessage("You will receive an e-mail with instructions to get you password back");

            return this.RedirectToPage("/Login/Login");
        }

        public class ForgotMyPasswordRequest
        {
            public string Email { get; set; }
        }
    }
}
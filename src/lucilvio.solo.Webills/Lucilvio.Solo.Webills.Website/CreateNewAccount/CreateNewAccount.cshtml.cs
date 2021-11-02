using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.Website.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.CreateNewAccount
{
    [AllowAnonymous]
    public class CreateNewAccountModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(CreateNewAccountRequest request, [FromServices]IUserAccountModule module)
        {
            var message = new CreateNewAccountMessage(request.Name, request.Email,
                request.Password, request.PasswordConfirmation, request.TermsAccepted);

            await module.SendMessage(message);

            this.SendSuccessMessage($"Welcome to WEBills {request.Name}! Now you can login and enjoy.");

            return this.RedirectToPage("/Login/Login");
        }

        public class CreateNewAccountRequest
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string PasswordConfirmation { get; set; }
            public bool TermsAccepted { get; set; }
        }
    }
}
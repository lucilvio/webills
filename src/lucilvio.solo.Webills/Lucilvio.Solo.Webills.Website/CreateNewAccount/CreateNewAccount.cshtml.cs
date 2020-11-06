using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.CreateAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lucilvio.Solo.Webills.Website.Shared;

namespace Lucilvio.Solo.Webills.Website.CreateNewAccount
{
    [AllowAnonymous]
    public class CreateNewAccountModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(CreateNewAccountRequest request, [FromServices]UserAccount.Module module)
        {
            await module.CreateNewAccount(request);

            this.SendSuccessMessage("Account successfully created!");

            return RedirectToPage("/Login/Login");
        }

        public class CreateNewAccountRequest : ICreateNewAccountMessage
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string PasswordConfirmation { get; set; }
            public bool TermsAccepted { get; set; }
        }
    }
}

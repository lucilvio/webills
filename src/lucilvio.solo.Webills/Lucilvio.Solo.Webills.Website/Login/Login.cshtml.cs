using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.Website.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Login
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public IActionResult OnGet()
        {
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(LoginRequest request, [FromServices] IAuthService authServie,
            [FromServices] IUserAccountModule module)
        {
            var message = new LoginMessage(request.Login, request.Password);
            await module.SendMessage(message);

            var loggedUser = message.Response;

            await authServie.SignIn(new UserAuthCredentials(loggedUser.Id, loggedUser.Name, loggedUser.Email));

            return this.RedirectToPage("/Home/Dashboard");
        }

        public class LoginRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
    }
}
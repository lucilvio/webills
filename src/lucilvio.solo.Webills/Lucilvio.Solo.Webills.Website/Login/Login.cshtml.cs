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
        private readonly Module _module;

        public LoginModel(Module module)
        {
            this._module = module;
        }

        public IActionResult OnGet()
        {
            if (this.User.Identity.IsAuthenticated)
                return this.RedirectToPage("/Home/Dashboard");

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(LoginRequest request, [FromServices] IAuthService authServie)
        {
            var loggedUser = await this._module.Login(new LoginMessage(request.Login, request.Password));

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
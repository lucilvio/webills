using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromServices]UserAccountModule userAccountModule, [FromForm]SignUpRequest request)
        {
            var input = new CreateUserAccountInput(request.Login, request.Password, request.PasswordConfirmation, request.Name,
                request.Login, request.TermsAccepted);

            await userAccountModule.CreateNewUserAccount(input);

            return this.RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}

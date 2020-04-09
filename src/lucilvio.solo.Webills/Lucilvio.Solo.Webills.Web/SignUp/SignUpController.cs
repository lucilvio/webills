using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        private readonly TransactionsModule _transactionsModule;

        public SignUpController(TransactionsModule transactionsModule)
        {
            this._transactionsModule = transactionsModule;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromServices]UserAccountModule userAccountModule, [FromForm]SignUpRequest request)
        {
            var input = new CreateUserAccountInput(request.Login, request.Password, request.PasswordConfirmation, request.Name,
                request.Login, request.TermsAccepted);

            await userAccountModule.CreateNewUserAccount(input, this.OnUserAccountCreated);

            return RedirectToAction(nameof(LoginController.Index), "Login");
        }

        private async Task OnUserAccountCreated(UserAccountCreated userAccountCreated)
        {
            await this._transactionsModule.CreateUser(new CreateUserInputAdapter(userAccountCreated));
        }
    }
}

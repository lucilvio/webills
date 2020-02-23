using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.Web.Logon;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        private readonly UserAccountModule _userAccountModule;

        public SignUpController(UserAccountModule userAccountModule)
        {
            this._userAccountModule = userAccountModule;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
            await this._userAccountModule.ExecuteCommand(new CreateUserAccountCommandAdapter(request));
            //await createUserAccount.Execute(, async createdUser =>
            //{
            //    await syncUser.Execute(createdUser.Id);
            //});

            return RedirectToAction(nameof(LogonController.Index), "Logon");
        }
    }
}

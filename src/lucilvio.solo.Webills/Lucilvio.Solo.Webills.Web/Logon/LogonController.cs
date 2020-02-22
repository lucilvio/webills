using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.Web.Home;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class LogonController : Controller
    {
        private readonly UserAccountModule _userAccountModule;

        public LogonController(UserAccountModule userAccountModule)
        {
            this._userAccountModule = userAccountModule;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LogonRequest request,
            [FromServices]IAuthentication authenticationService)
        {
            await this._userAccountModule.ExecuteCommand(new LoginCommandAdapter(request));
            //await useCase.Execute(, async user =>
            //{
            //    await authenticationService.SignIn(new AuthCredentials(user.Id, user.Login, user.Name));
            //});

            return RedirectToAction(nameof(HomeController.Dashboard), "Home");
        }
    }
}
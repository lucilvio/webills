using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.Web.Home;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class LogonController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LogonRequest request, [FromServices]ILoginUseCase useCase,
            [FromServices]IAuthentication authenticationService)
        {
            await useCase.Execute(new LoginCommandAdapter(request), async user =>
            {
                await authenticationService.SignIn(new AuthCredentials(user.Id, user.Login, user.Name));
            });

            return RedirectToAction(nameof(HomeController.Dashboard), "Home");
        }
    }
}
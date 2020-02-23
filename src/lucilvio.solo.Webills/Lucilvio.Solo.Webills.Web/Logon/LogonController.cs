using System.Text.Json;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Bus;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.Web.Home;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class LogonController : Controller
    {
        private readonly IBus _bus;
        private readonly IAuthentication _authService;

        private readonly UserAccountModule _userAccountModule;

        public LogonController(UserAccountModule userAccountModule, IAuthentication authService, IBus bus)
        {
            this._bus = bus;
            this._bus.Subscribe<LogonController>("UserLoggedInEvent", this.UserLoggedInEventHandler);

            this._authService = authService;
            this._userAccountModule = userAccountModule;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LogonRequest request)
        {
            await this._userAccountModule.ExecuteCommand(new LoginCommandAdapter(request));

            return RedirectToAction(nameof(HomeController.Dashboard), "Home");
        }

        private async Task UserLoggedInEventHandler(string evt)
        {
            var loggedUser = JsonSerializer.Deserialize<UserLoggedInEvent>(evt);
            await this._authService.SignIn(new AuthCredentials(loggedUser.Id, loggedUser.Login, loggedUser.Name));
        }
    }
}
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.Web.Home;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    public partial class LoginController : Controller
    {
        private readonly IAuthentication _authService;

        public LoginController(IAuthentication authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromServices]UserAccountModule userAccountModule, [FromForm]LoginRequest request)
        {
            await userAccountModule.Login(new LoginInput(request.Login, request.Password), this.OnUserLogin);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task OnUserLogin(LoggedUser loggedUser)
        {
            await this._authService.SignIn(new UserAuthCredentials(loggedUser.Id, loggedUser.Login, loggedUser.Name));
        }
    }
}
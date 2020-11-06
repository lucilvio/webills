using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.Web.Home;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    public class LoginController : Controller
    {
        private readonly Module _module;

        public LoginController(Module module)
        {
            this._module = module;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            await this._module.Login(request);
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }


    }
}
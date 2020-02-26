using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.SignOut
{
    [Authorize]
    [Route("Logoff")]
    public class LogoffController : Controller
    {
        private readonly IAuthentication _securityService;

        public LogoffController(IAuthentication securityService)
        {
            this._securityService = securityService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            await this._securityService.SignOut();

            return RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}
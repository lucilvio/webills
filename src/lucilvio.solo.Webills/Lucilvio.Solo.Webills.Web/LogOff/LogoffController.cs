using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lucilvio.Solo.Webills.Web.Logon;
using Microsoft.AspNetCore.Authorization;

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

            return RedirectToAction(nameof(LogonController.Index), "Logon");
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lucilvio.Solo.Webills.Web.Logon;
using Microsoft.AspNetCore.Authorization;

namespace Lucilvio.Solo.Webills.Web.SignOut
{
    [Authorize]
    public class SignOutController : Controller
    {
        private readonly IAuthentication _securityService;

        public SignOutController(IAuthentication securityService)
        {
            this._securityService = securityService;
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await this._securityService.SignOut();

            return RedirectToAction(nameof(LogonController.Index), "Logon");
        }
    }
}
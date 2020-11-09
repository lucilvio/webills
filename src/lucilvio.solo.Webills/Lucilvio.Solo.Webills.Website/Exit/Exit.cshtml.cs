using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Website.Shared.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Exit
{
    public class ExitModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync([FromServices] IAuthService authService)
        {
            await authService.SignOut();

            return this.RedirectToPage("/Login/Login");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Exit
{
    public class ExitModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync([FromServices] IHttpContextAccessor contextAcessor)
        {
            await contextAcessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return this.RedirectToPage("/Login/Login");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.ForgotMyPassword
{
    [AllowAnonymous]
    public class ForgotMyPasswordModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

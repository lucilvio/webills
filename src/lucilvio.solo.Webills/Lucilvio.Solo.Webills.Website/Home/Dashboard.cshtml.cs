using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Home
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}

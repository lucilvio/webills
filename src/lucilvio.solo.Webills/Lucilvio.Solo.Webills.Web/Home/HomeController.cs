using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

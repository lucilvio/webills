using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Shared
{
    public static class Messages
    {
        public static void SendSuccessMessage(this PageModel pageModel, string message)
        {
            pageModel.TempData["successMessage"] = message;
        }

        public static void SendErrorMessage(this PageModel pageModel, string message)
        {
            pageModel.TempData["successMessage"] = message;
        }
    }
}

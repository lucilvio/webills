
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Messages
{
    public static class Messages
    {
        public static void SendSuccessMessage(this Controller controller, string message)
        {
            controller.TempData["successMessage"] = message;
        }
    }
}
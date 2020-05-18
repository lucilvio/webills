using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Filters
{
    public class ErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //var action = context.RouteData.Values["action"]?.ToString();
            //var controller = context.RouteData.Values["controller"].ToString();

            //var viewResult = new ViewResult();
            //var d = new Result
            //var redirect = new RedirectToActionResult("Index", controller, null);
            //context.ExceptionHandled = true;
            //context.Result = ;
        }
    }
}

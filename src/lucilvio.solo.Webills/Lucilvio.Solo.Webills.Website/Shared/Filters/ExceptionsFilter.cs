using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Lucilvio.Solo.Webills.Website.Shared.Filters
{
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(Exception))
            {
                await base.OnExceptionAsync(context);
                return;
            }

            var tempDataFactory = (ITempDataDictionaryFactory)context.HttpContext?.RequestServices.GetService(typeof(ITempDataDictionaryFactory));

            var tempData = tempDataFactory.GetTempData(context.HttpContext);
            tempData["errorMessage"] = context.Exception.GetType().Name;

            context.ExceptionHandled = true;
            context.Result = new RedirectToPageResult(((Microsoft.AspNetCore.Mvc.RazorPages.PageActionDescriptor)context.ActionDescriptor).ViewEnginePath);
        }
    }
}

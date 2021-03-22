using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace Lucilvio.Solo.Webills.Website.Shared.Filters
{
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ExceptionsFilter(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger("ExceptionFilter");
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var tempDataFactory = (ITempDataDictionaryFactory)context.HttpContext?.RequestServices.GetService(typeof(ITempDataDictionaryFactory));
            var tempData = tempDataFactory.GetTempData(context.HttpContext);

            context.ExceptionHandled = true;

            var message = "";

            if (context.Exception.GetType() == typeof(BusinessException))
            {
                message = context.Exception.GetType().Name;
            }
            else
            {
                if (this._logger != null)
                    _logger.LogError(context.Exception, "Action {action}", context.ActionDescriptor.DisplayName);

                message = "There was a internal error. Please, try again later.";
            }

            tempData["errorMessage"] = message;
            context.Result = new RedirectToPageResult(((Microsoft.AspNetCore.Mvc.RazorPages.PageActionDescriptor)context.ActionDescriptor).ViewEnginePath);
        }
    }
}
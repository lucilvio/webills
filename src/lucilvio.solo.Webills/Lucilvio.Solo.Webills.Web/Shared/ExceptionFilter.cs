using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ITempDataDictionaryFactory _tempDataFactory;

        public ExceptionFilter(ITempDataDictionaryFactory tempDataFactory)
        {
            this._tempDataFactory = tempDataFactory;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(Exception))
                return;

            var tempData = this._tempDataFactory.GetTempData(context.HttpContext);
            tempData["errorMessage"] = ExceptionTranslation.Translate(context.Exception);

            context.ExceptionHandled = true;
            var defaultRedirectAction = "Index";
            var defaultRedirectController = context.RouteData.Values["controller"] != null ? context.RouteData.Values["controller"].ToString() : "Error";

            context.Result = new RedirectToActionResult(defaultRedirectAction, defaultRedirectController, new { });
        }
    }
}
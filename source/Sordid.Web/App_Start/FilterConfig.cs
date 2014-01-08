using Elmah;
using System.Web.Mvc;

namespace Sordid.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Without this, ELMAH does not intercept errors that are handled
            // by the MVC custom error pages.
            filters.Add(new ElmahInterceptingHandleErrorAttribute());
        }
    }

    public class ElmahInterceptingHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            if (!context.ExceptionHandled)
                return;
            var httpContext = context.HttpContext.ApplicationInstance.Context;
            var signal = ErrorSignal.FromContext(httpContext);
            signal.Raise(context.Exception, httpContext);
        }
    }
}

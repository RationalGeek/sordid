using System.Web;
using System.Web.Mvc;

namespace Sordid.Web
{
    public class JsonErrorHandlingAttribute : HandleErrorAttribute
    {
        // http://stackoverflow.com/questions/19666988/asp-net-mvc-ajax-error-returning-view-instead-of-ajax

        public override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            var statusCode = new HttpException(null, exception).GetHttpCode();

            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    message = filterContext.Exception.Message
                }
            };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
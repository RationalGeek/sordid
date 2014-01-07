using System;
using System.Web.Mvc;

namespace Sordid.Web
{
    /// <summary>
    /// Date parsing in JSON is the suck.  See this for more info:
    /// http://www.hanselman.com/blog/OnTheNightmareThatIsJSONDatesPlusJSONNETAndASPNETWebAPI.aspx
    /// http://www.devcurry.com/2013/04/json-dates-are-different-in-aspnet-mvc.html
    /// http://stackoverflow.com/questions/20980241/how-do-i-sub-in-json-net-as-model-binder-for-asp-net-mvc-controllers
    /// </summary>
    public class CustomDateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var rawValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).RawValue.ToString();
            var regex = new System.Text.RegularExpressions.Regex(@"\\?/Date\((\d+)\)/\\?");
            var match = regex.Match(rawValue);
            if (match.Success)
            {
                var jsonDateValue = match.Groups[1].Value;
                var theDate = new DateTime(1970,1,1,0,0,0,0).AddMilliseconds(Double.Parse(jsonDateValue)).AddHours(-8);
                return theDate;
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}

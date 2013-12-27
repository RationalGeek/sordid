using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sordid.Web.Startup))]
namespace Sordid.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

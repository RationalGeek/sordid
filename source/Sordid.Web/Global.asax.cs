using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Sordid.Core;
using Sordid.Core.Model;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sordid.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<SordidDbContext>(new SordidDatabaseInitializer<SordidDbContext>());
        }

        // TODO: Below DB init crap seems a bit hacky. Fix it.
        private bool _dbInitialized = false;
        protected void Application_PostAuthenticateRequest(object sender, System.EventArgs e)
        {
            if (!_dbInitialized)
            {
                _dbInitialized = true;

                // Force the DB to initialize in startup as opposed to whenever we randomly do the first query
                var ctx = new SordidDbContext();
                var count = ctx.Users.Count();
            }
        }
    }

    public class SordidDatabaseInitializer<T> : DropCreateDatabaseIfModelChanges<T>
        where T : DbContext
    {
        protected override void Seed(T context)
        {
            base.Seed(context);

            // TODO: Delete test user stuff before prod.  Makes dev round tripping easier.

            var logger = new Ninject.Extensions.Logging.Log4net.Infrastructure.Log4NetLogger(this.GetType());
            logger.Info("Database re-initializing.  Creating test user and logging in.");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser { UserName = "RationalGeek" };
            var result = userManager.Create(user, "password");
            context.SaveChanges();

            var authMgr = HttpContext.Current.GetOwinContext().Authentication;
            authMgr.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authMgr.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
            System.Threading.Thread.CurrentPrincipal = authMgr.User;
        }
    }
}

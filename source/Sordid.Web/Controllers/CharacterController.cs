using Ninject.Extensions.Logging;
using System.Web.Mvc;

namespace Sordid.Web.Controllers
{
    public class CharacterController : Controller
    {
        private ILogger _logger;

        public CharacterController(ILogger logger)
        {
            _logger = logger;
        }

        // GET: /Character/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Character/New
        public ActionResult New()
        {
            // TODO: Delete this test log entry
            // TODO: Logs not getting written because there is no log4net config
            // TODO: Test that Glimpse log4net extension is working
            // TODO: Why isn't the Glimpse EF6 extension working?  Issue some SQL here to test it out.
            // TODO: Add ELMAH
            _logger.Info("Rendering new character view...");

            return View();
        }
	}
}
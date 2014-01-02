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
            return View();
        }
    }
}
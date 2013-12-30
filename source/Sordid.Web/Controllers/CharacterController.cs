using System.Web.Mvc;

namespace Sordid.Web.Controllers
{
    public class CharacterController : Controller
    {
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
using Sordid.Web.Models;
using System.Web.Mvc;

namespace Sordid.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View(new AboutViewModel());
        }

        public ActionResult EmptyTest()
        {
            return View();
        }
    }
}
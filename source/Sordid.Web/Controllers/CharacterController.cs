using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using System.Web.Mvc;

namespace Sordid.Web.Controllers
{
    public class CharacterController : Controller
    {
        private ILogger _logger;
        private ICharacterService _characterService;

        public CharacterController(ICharacterService characterService, ILogger logger)
        {
            _characterService = characterService;
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
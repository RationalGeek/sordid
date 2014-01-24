using Ninject.Extensions.Logging;
using Sordid.Core.Exceptions;
using Sordid.Core.Interfaces;
using Sordid.Web.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sordid.Web.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private ILogger _logger;
        private ICharacterService _characterService;
        private IPowerService _powerService;

        public CharacterController(ICharacterService characterService, ILogger logger, IPowerService powerService)
        {
            _characterService = characterService;
            _powerService = powerService;
            _logger = logger;
        }

        // GET: /Character/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Character/New
        public async Task<ActionResult> New()
        {
            var character = await _characterService.NewCharacter();
            return RedirectToAction("Manage", new { id = character.Id });
        }

        // GET: /Character/Manage
        public async Task<ActionResult> Manage(int id)
        {
            var character = await _characterService.LoadCharacter(id);
            if (character == null)
                throw new SordidException(string.Format("Character ID {0} does not exist.", id));
            return View(new ManageCharacterViewModel { Character = character });
        }

        // POST: /Character/Save
        [HttpPost]
        [JsonErrorHandling]
        public async Task<ActionResult> Save(ManageCharacterViewModel characterVM)
        {
            characterVM.Character = await _characterService.SaveCharacter(characterVM.Character);
            return Json(characterVM);
        }

        // GET: /Character/AddPowerDialog
        public async Task<ActionResult> AddPowerDialog()
        {
            var powers = await _powerService.GetStockPowers();

            return PartialView("_AddPowerDialog", new AddPowerDialogViewModel { StockPowers = powers });
        }
    }
}
using Ninject.Extensions.Logging;
using Sordid.Core.Exceptions;
using Sordid.Core.Interfaces;
using Sordid.Web.Models;
using System.Linq;
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
        private IPowerLevelService _powerLevelService;
        private ITemplateService _templateService;

        public CharacterController(ICharacterService characterService, ILogger logger, IPowerService powerService, IPowerLevelService powerLevelService, ITemplateService templateService)
        {
            _characterService = characterService;
            _powerService = powerService;
            _powerLevelService = powerLevelService;
            _templateService = templateService;
            _logger = logger;
        }

        // GET: /Character/
        public async Task<ActionResult> Index()
        {
            var characters = (await _characterService.LoadCharactersForCurrentUser())
                .Select(c => new CharacterListViewModel(c)).ToList();
            return View(characters);
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
            var powerLevels = (await _powerLevelService.GetAllPowerLevels()).ToList();
            var templates = (await _templateService.GetAllTemplates()).ToList();
            return View(new ManageCharacterViewModel { Character = character, PowerLevels = powerLevels, Templates = templates });
        }

        // GET: /Character/Print
        public async Task<ActionResult> Print(int id)
        {
            var character = await _characterService.LoadCharacter(id);
            if (character == null)
                throw new SordidException(string.Format("Character ID {0} does not exist.", id));
            return View(new PrintCharacterViewModel { Character = character });
        }

        // POST: /Character/Save
        [HttpPost]
        [JsonErrorHandling]
        public async Task<ActionResult> Save(ManageCharacterViewModel characterVM)
        {
            characterVM.Character = await _characterService.SaveCharacter(characterVM.Character);
            return Json(characterVM);
        }

        // POST: /Character/Delete
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _characterService.DeleteCharacter(id);
            return RedirectToAction("Index");
        }

        // GET: /Character/AddPowerDialog
        public async Task<ActionResult> AddPowerDialog()
        {
            var powers = await _powerService.GetStockPowers();

            return PartialView("_AddPowerDialog", new AddPowerDialogViewModel { StockPowers = powers });
        }
    }
}
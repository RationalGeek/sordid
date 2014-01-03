using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class CharacterService : ICharacterService
    {
        private ICharacterRepository _charRepo;
        private IUserService _userService;

        public CharacterService(ICharacterRepository charRepo, IUserService userService)
        {
            _charRepo = charRepo;
            _userService = userService;
        }

        public async Task<Character> NewCharacter()
        {
            var userId = (await _userService.GetCurrentUser()).Id;
            var character = new Character { Name = "New Character", ApplicationUserId = userId };
            character = _charRepo.Add(character);
            await _charRepo.UnitOfWork.Save();
            return character;
        }

        public async Task<Character> LoadCharacter(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var result = (await _charRepo.Find(c => c.Id == id && c.ApplicationUserId == userId)).FirstOrDefault();
            return result;
        }
    }
}

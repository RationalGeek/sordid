using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class CharacterService : ICharacterService
    {
        private ICharacterRepository _charRepo;
        private IUserService _userService;
        private ISkillService _skillService;
        private IAspectService _aspectService;

        public CharacterService(ICharacterRepository charRepo, IUserService userService, ISkillService skillService, IAspectService aspectService)
        {
            _charRepo = charRepo;
            _userService = userService;
            _skillService = skillService;
            _aspectService = aspectService;
        }

        public async Task<Character> NewCharacter()
        {
            var userId = (await _userService.GetCurrentUser()).Id;
            var character = new Character
                {
                    Name = "New Character",
                    ApplicationUserId = userId,
                    PhysicalStress = 2,
                    MentalStress = 2,
                    SocialStress = 2,
                    RandomHash = CreateRandomHash(),
                };
            await InitSkills(character);
            await InitAspects(character);
            InitConsequences(character);
            character.Powers = new List<CharacterPower>();

            character = _charRepo.Add(character);
            await _charRepo.UnitOfWork.Save();
            return character;
        }

        private string CreateRandomHash()
        {
            var guid = Guid.NewGuid();
            var bytes = guid.ToByteArray();
            var hash = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private async Task InitSkills(Character character)
        {
            character.Skills = (await _skillService.GetStandardSkills()).Select(s => new CharacterSkill
                {
                    SkillId = s.Id,
                    Skill = s,
                    Rank = 0,
                }).ToList();
        }

        private async Task InitAspects(Character character)
        {
            var i = 0;
            character.Aspects = (await _aspectService.GetStandardAspects()).Select(a => new CharacterAspect
            {
                AspectId = a.Id,
                Aspect = a,
                Name = "Aspect #" + ++i,
            }).ToList();
        }

        private void InitConsequences(Character character)
        {
            character.Consequences = new List<Consequence>
            {
                new Consequence { Type = "Mild"    , StressType = "Any", StressAmount = -2, UserCreated = false },
                new Consequence { Type = "Moderate", StressType = "Any", StressAmount = -4, UserCreated = false },
                new Consequence { Type = "Severe"  , StressType = "Any", StressAmount = -6, UserCreated = false },
                new Consequence { Type = "Extreme" , StressType = "Any", StressAmount = -8, UserCreated = false },
            };
        }

        public async Task<List<Character>> LoadCharactersForCurrentUser()
        {
            var userId = _userService.GetCurrentUserId();
            var query = _charRepo
                            .GetQueryable()
                            .Where(c => c.ApplicationUserId == userId)
                            .OrderByDescending(c => c.DateUpdated);
            var result = (await _charRepo.Query(query)).ToList();
            return result;
        }

        public async Task<Character> LoadCharacter(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var query = _charRepo
                            .GetQueryable()
                            .Where(c => c.Id == id && c.ApplicationUserId == userId)
                            .Include(c => c.Aspects)
                            .Include(c => c.Aspects.Select(a => a.Aspect))
                            .Include(c => c.Skills)
                            .Include(c => c.Skills.Select(s => s.Skill))
                            .Include(c => c.Powers)
                            .Include(c => c.Powers.Select(p => p.Power))
                            .Include(c => c.PowerLevel)
                            .Include(c => c.Consequences);
            var result = (await _charRepo.Query(query)).SingleOrDefault();
            return result;
        }

        public async Task<Character> SaveCharacter(Character character)
        {
            _charRepo.Update(character);
            await _charRepo.UnitOfWork.Save();
            return await LoadCharacter(character.Id);
        }

        public async Task DeleteCharacter(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var query = _charRepo
                            .GetQueryable()
                            .Where(c => c.Id == id && c.ApplicationUserId == userId);
            var characterToDelete = (await _charRepo.Query(query)).SingleOrDefault();
            if (characterToDelete != null)
            {
                _charRepo.Delete(characterToDelete);
                await _charRepo.UnitOfWork.Save();
            }
        }
    }
}

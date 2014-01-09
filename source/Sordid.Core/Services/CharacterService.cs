using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using Sordid.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

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
            InitSkills(character);
            character = _charRepo.Add(character);
            await _charRepo.UnitOfWork.Save();
            return character;
        }

        private void InitSkills(Character character)
        {
            character.Skills = new List<Skill>
            {
                // TODO: This stuff should probably be in a config file somewhere
                // TODO: Normalize this better?  Skills / Trappings maybe should be separate LOV table?
                new Skill { Name = "Alertness", Trappings = "Avoiding Surprise, Combat Initiative, Passive Awareness" },
                new Skill { Name = "Athletics", Trappings = "Climbing, Dodging, Falling, Jumping, Sprinting, Other Physical Actions" },
                new Skill { Name = "Burglary", Trappings = "Casing, Infiltration, Lockpicking" },
                new Skill { Name = "Contacts", Trappings = "Gathering Information, Getting the Tip-Off, Knowing People, Rumors" },
                new Skill { Name = "Conviction", Trappings = "Acts of Faith, Mental Fortitude" },
                new Skill { Name = "Craftsmanship", Trappings = "Breaking, Building, Fixing" },
                new Skill { Name = "Deceit", Trappings = "Cat and Mouse, Disguise, Distraction and Misdirection, False Face Forward, Falsehood and Deception" },
                new Skill { Name = "Discipline", Trappings = "Concentration, Emotional Control, Mental Defense"},
                new Skill { Name = "Driving", Trappings = "Chases, One Hand on the Wheel, Other Vehicles, Street Knowledge and Navigation"},
                new Skill { Name = "Empathy", Trappings = "Reading People, A Shoulder to Cry On, Social Defense, Social Initiative"},
                new Skill { Name = "Endurance", Trappings = "Long-Term Action, Physical Fortitude"},
                new Skill { Name = "Fists", Trappings = "Brawling, Close-Combat Defense"},
                new Skill { Name = "Guns", Trappings = "Aiming, Gun Knowledge, Gunplay, Other Projectile Weapons"},
                new Skill { Name = "Intimidation", Trappings = "The Brush-Off, Interrogation, Provocation, Social Attacks, Threats"},
                new Skill { Name = "Investigation", Trappings = "Eavesdropping, Examination, Surveillance"},
                new Skill { Name = "Lore", Trappings = "Arcane Research, Common Ritual, Mystic Perception"},
                new Skill { Name = "Might", Trappings = "Breaking Things, Exerting Force, Lifting Things, Wrestling"},
                new Skill { Name = "Performance", Trappings = "Art Appreciation, Composition, Creative Communication, Playing to an Audience"},
                new Skill { Name = "Presence", Trappings = "Charisma, Command, Reputation, Social Fortitude"},
                new Skill { Name = "Rapport", Trappings = "Chit-Chat, Closing Down, First Impressions, Opening Up, Social Defense"},
                new Skill { Name = "Resources", Trappings = "Buying Things, Equipment, Lifestyle, Money Talks, Workspaces"},
                new Skill { Name = "Scholarship", Trappings = "Answers, Computer Use, Declaring Minor Details, Exposition and Knowledge Dumping, Languages, Medical Attention, Research and Lab Work"},
                new Skill { Name = "Stealth", Trappings = "Ambush, Hiding, Shadowing, Skulking"},
                new Skill { Name = "Survival", Trappings = "Animal Handling, Camouflage, Riding, Scavenging, Tracking"},
                new Skill { Name = "Weapons", Trappings = "Melee Combat, Melee Defense, Distance Weaponry, Weapon Knowledge"},
            };
        }

        public async Task<Character> LoadCharacter(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var query = _charRepo
                            .GetQueryable()
                            .Where(c => c.Id == id && c.ApplicationUserId == userId)
                            .Include(c => c.Skills);
            var result = (await _charRepo.Query(query)).SingleOrDefault();
            return result;
        }

        public async Task<Character> SaveCharacter(Character character)
        {
            character = _charRepo.Update(character);
            await _charRepo.UnitOfWork.Save();
            return character;
        }
    }
}

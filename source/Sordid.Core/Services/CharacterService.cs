using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class CharacterService : ICharacterService
    {
        private ICharacterRepository _charRepo;
        private IUserService _userService;
        private ISkillService _skillService;

        public CharacterService(ICharacterRepository charRepo, IUserService userService, ISkillService skillService)
        {
            _charRepo = charRepo;
            _userService = userService;
            _skillService = skillService;
        }

        public async Task<Character> NewCharacter()
        {
            var userId = (await _userService.GetCurrentUser()).Id;
            var character = new Character { Name = "New Character", ApplicationUserId = userId };
            await InitSkills(character);
            //InitAspects(character);
            character = _charRepo.Add(character);
            await _charRepo.UnitOfWork.Save();
            return character;
        }

        private async Task InitSkills(Character character)
        {
            character.Skills = (await _skillService.GetStandardSkills()).Select(s => new CharacterSkill
                {
                    SkillId = s.Id,
                    Skill = s,
                    Rank = 0,
                }).ToList();

            //character.Skills = new List<Skill>
            //{
            //    // TODO: This stuff should probably be in a config file somewhere
            //    // TODO: Normalize this better?  Skills / Trappings maybe should be separate LOV table?
            //    new CharacterSkill { Name = "Alertness", Trappings = "Avoiding Surprise, Combat Initiative, Passive Awareness" },
            //    new CharacterSkill { Name = "Athletics", Trappings = "Climbing, Dodging, Falling, Jumping, Sprinting, Other Physical Actions" },
            //    new CharacterSkill { Name = "Burglary", Trappings = "Casing, Infiltration, Lockpicking" },
            //    new CharacterSkill { Name = "Contacts", Trappings = "Gathering Information, Getting the Tip-Off, Knowing People, Rumors" },
            //    new CharacterSkill { Name = "Conviction", Trappings = "Acts of Faith, Mental Fortitude" },
            //    new CharacterSkill { Name = "Craftsmanship", Trappings = "Breaking, Building, Fixing" },
            //    new CharacterSkill { Name = "Deceit", Trappings = "Cat and Mouse, Disguise, Distraction and Misdirection, False Face Forward, Falsehood and Deception" },
            //    new CharacterSkill { Name = "Discipline", Trappings = "Concentration, Emotional Control, Mental Defense"},
            //    new CharacterSkill { Name = "Driving", Trappings = "Chases, One Hand on the Wheel, Other Vehicles, Street Knowledge and Navigation"},
            //    new CharacterSkill { Name = "Empathy", Trappings = "Reading People, A Shoulder to Cry On, Social Defense, Social Initiative"},
            //    new CharacterSkill { Name = "Endurance", Trappings = "Long-Term Action, Physical Fortitude"},
            //    new CharacterSkill { Name = "Fists", Trappings = "Brawling, Close-Combat Defense"},
            //    new CharacterSkill { Name = "Guns", Trappings = "Aiming, Gun Knowledge, Gunplay, Other Projectile Weapons"},
            //    new CharacterSkill { Name = "Intimidation", Trappings = "The Brush-Off, Interrogation, Provocation, Social Attacks, Threats"},
            //    new CharacterSkill { Name = "Investigation", Trappings = "Eavesdropping, Examination, Surveillance"},
            //    new CharacterSkill { Name = "Lore", Trappings = "Arcane Research, Common Ritual, Mystic Perception"},
            //    new CharacterSkill { Name = "Might", Trappings = "Breaking Things, Exerting Force, Lifting Things, Wrestling"},
            //    new CharacterSkill { Name = "Performance", Trappings = "Art Appreciation, Composition, Creative Communication, Playing to an Audience"},
            //    new CharacterSkill { Name = "Presence", Trappings = "Charisma, Command, Reputation, Social Fortitude"},
            //    new CharacterSkill { Name = "Rapport", Trappings = "Chit-Chat, Closing Down, First Impressions, Opening Up, Social Defense"},
            //    new CharacterSkill { Name = "Resources", Trappings = "Buying Things, Equipment, Lifestyle, Money Talks, Workspaces"},
            //    new CharacterSkill { Name = "Scholarship", Trappings = "Answers, Computer Use, Declaring Minor Details, Exposition and Knowledge Dumping, Languages, Medical Attention, Research and Lab Work"},
            //    new CharacterSkill { Name = "Stealth", Trappings = "Ambush, Hiding, Shadowing, Skulking"},
            //    new CharacterSkill { Name = "Survival", Trappings = "Animal Handling, Camouflage, Riding, Scavenging, Tracking"},
            //    new CharacterSkill { Name = "Weapons", Trappings = "Melee Combat, Melee Defense, Distance Weaponry, Weapon Knowledge"},
            //};
        }

        //private void InitAspects(Character character)
        //{
        //    character.Aspects = new List<Aspect>
        //    {
        //        new Aspect { MainLabel = "High Concept Aspect" },
        //        new Aspect { MainLabel = "Trouble Aspect" },
        //        new PhaseAspect
        //        {
        //            PhaseName = "Phase One",
        //            HeadingLabel = "Background",
        //            SubHeadingLabel = "Where did you come from?",
        //            DescriptiveBlurb = "What nation, region, culture are you from? What were your family circumstances like? What's your relationship with your family? How were you educated? What were your friends like? Did you get into trouble much? If you're supernatural, how early did you learn this? Were there problems?",
        //        },
        //        new PhaseAspect
        //        {
        //            PhaseName = "Phase Two",
        //            HeadingLabel = "Rising Conflict",
        //            SubHeadingLabel = "What shaped you?",
        //            DescriptiveBlurb = "What nation, region, culture are you from? What were your family circumstances like? What's your relationship with your family? How were you educated? What were your friends like? Did you get into trouble much? If you're supernatural, how early did you learn this? Were there problems?",
        //        },
        //    };
        //}

        public async Task<Character> LoadCharacter(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var query = _charRepo
                            .GetQueryable()
                            .Where(c => c.Id == id && c.ApplicationUserId == userId)
                            //.Include(c => c.Aspects)
                            .Include(c => c.Skills)
                            .Include(c => c.Skills.Select(s => s.Skill));
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

using Sordid.Core.Interfaces;
using System.Collections.Generic;
namespace Sordid.Core.Model
{
    public class Character : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlayerName { get; set; }
        public string ImageUrl { get; set; }
        public int MaxSkillPoints { get; set; }
        public int BaseRefresh { get; set; }
        public string Notes { get; set; }

        public int PhysicalStress { get; set; }
        public int MentalStress { get; set; }
        public int SocialStress { get; set; }

        public Template Template { get; set; }
        public int? TemplateId { get; set; }

        public PowerLevel PowerLevel { get; set; }
        public int? PowerLevelId { get; set; }

        public List<CharacterSkill> Skills { get; set; }
        public List<CharacterAspect> Aspects { get; set; }
        public List<CharacterPower> Powers { get; set; }
        public List<Consequence> Consequences { get; set; }

        /// <summary>
        /// Random Base64 MD5 hash used to seed things for the character
        /// that needs to be different than other characters.  Stable over
        /// the lifetime of the character.  For example, used to "randomize"
        /// the foreground and background color of the default portrait.
        /// </summary>
        public string RandomHash { get; set; }

        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}

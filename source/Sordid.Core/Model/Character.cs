using Sordid.Core.Interfaces;
using System.Collections.Generic;
namespace Sordid.Core.Model
{
    public class Character : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlayerName { get; set; }
        public string Appearance { get; set; }
        public string Notes { get; set; }
        public string StoryTitle { get; set; }
        public string Starring { get; set; }
        public string ImageUrl { get; set; }
        public int MaxSkillPoints { get; set; }
        public int BaseRefresh { get; set; }

        public PowerLevel PowerLevel { get; set; }
        public int? PowerLevelId { get; set; }

        public Template Template { get; set; }
        public int? TemplateId { get; set; }

        public List<CharacterSkill> Skills { get; set; }
        public List<CharacterAspect> Aspects { get; set; }
        public List<CharacterPower> Powers { get; set; }

        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}

using Sordid.Core.Interfaces;

namespace Sordid.Core.Model
{
    public class Skill : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public SkillType Type { get; set; }
        public string Name { get; set; }
        public string Trappings { get; set; }
    }

    public enum SkillType
    {
        Standard,
        Custom,
    }
}

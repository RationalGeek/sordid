using Sordid.Core.Interfaces;
using System.Web.Script.Serialization;

namespace Sordid.Core.Model
{
    public class CharacterSkill : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public int Rank { get; set; }

        public int CharacterId { get; set; }
        [ScriptIgnore] // TODO: Ew, fix this JSON ignore attribute somehow to avoid circular references...
        public Character Character { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}

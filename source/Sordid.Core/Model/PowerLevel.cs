
using Sordid.Core.Interfaces;
namespace Sordid.Core.Model
{
    public class PowerLevel : IIdKeyedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseRefresh { get; set; }
        public int SkillPoints { get; set; }
        public int MaxSkillRank { get; set; }
    }
}

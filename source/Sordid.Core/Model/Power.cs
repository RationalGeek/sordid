using Sordid.Core.Interfaces;
using System.Web.Script.Serialization;

namespace Sordid.Core.Model
{
    public class Power : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }

        public int CharacterId { get; set; }
        [ScriptIgnore] // TODO: Ew, fix this JSON ignore attribute somehow to avoid circular references...
        public Character Character { get; set; }
    }
}

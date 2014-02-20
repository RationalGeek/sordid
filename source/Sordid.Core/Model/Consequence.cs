using Sordid.Core.Interfaces;
using System.Web.Script.Serialization;

namespace Sordid.Core.Model
{
    public class Consequence : IIdKeyedEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string StressType { get; set; }
        public int StressAmount { get; set; }
        public bool UserCreated { get; set; }

        public int CharacterId { get; set; }
        [ScriptIgnore] // TODO: Ew, fix this JSON ignore attribute somehow to avoid circular references...
        public Character Character { get; set; }
    }
}

using Sordid.Core.Interfaces;

namespace Sordid.Core.Model
{
    public class Power : BaseEntity, IIdKeyedEntity
    {
        public PowerType Type { get; set; }
        public string TypeName { get { return Type.ToString(); } }

        public int Id { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }

    public enum PowerType
    {
        Stock,
        Custom
    }
}

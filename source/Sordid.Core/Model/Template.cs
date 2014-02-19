using Sordid.Core.Interfaces;

namespace Sordid.Core.Model
{
    public class Template : IIdKeyedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

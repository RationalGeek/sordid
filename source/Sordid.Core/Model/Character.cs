using Sordid.Core.Interfaces;
namespace Sordid.Core.Model
{
    public class Character : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlayerName { get; set; }

        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}

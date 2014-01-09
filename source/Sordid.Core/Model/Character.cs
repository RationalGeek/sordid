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

        public List<Skill> Skills { get; set; }

        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}

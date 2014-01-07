using System;

namespace Sordid.Core.Model
{
    public abstract class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        // TODO: Optimistic checking with Timestamp not working with JSON round tripping
        //[Timestamp]//, ConcurrencyCheck]
        //public byte[] ConcurrencyVersion { get; set; }
    }
}

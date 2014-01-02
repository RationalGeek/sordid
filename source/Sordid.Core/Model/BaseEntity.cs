using System;
using System.ComponentModel.DataAnnotations;

namespace Sordid.Core.Model
{
    public abstract class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [Timestamp]//, ConcurrencyCheck]
        public byte[] ConcurrencyVersion { get; set; }
    }
}

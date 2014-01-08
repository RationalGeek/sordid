using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sordid.Core.Model
{
    public abstract class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        private byte[] _ConcurrencyVersion;
        [Timestamp]
        public byte[] ConcurrencyVersion
        {
            get
            {
                return _ConcurrencyVersion;
            }
            set
            {
                _ConcurrencyVersion = value;
                if (value != null)
                    ConcurrencyVersionBase64 = Convert.ToBase64String(value);
            }
        }

        /// <summary>
        /// Allows for concurrency tracking column to be round-tripped through JSON.
        /// </summary>
        [NotMapped]
        public string ConcurrencyVersionBase64 { get; set; }
    }
}

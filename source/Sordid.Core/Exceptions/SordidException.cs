using System;

namespace Sordid.Core.Exceptions
{
    public class SordidException : Exception
    {
        public SordidException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}

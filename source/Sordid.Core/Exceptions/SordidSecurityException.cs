using System;

namespace Sordid.Core.Exceptions
{
    public class SordidSecurityException : Exception
    {
        public SordidSecurityException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}

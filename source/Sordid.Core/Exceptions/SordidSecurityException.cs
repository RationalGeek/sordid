using System;

namespace Sordid.Core.Exceptions
{
    public class SordidSecurityException : SordidException
    {
        public SordidSecurityException(string message, Exception innerException = null) 
            : base(message, innerException)
        {
        }
    }
}

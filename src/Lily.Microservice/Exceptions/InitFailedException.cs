using System;

namespace Lily.Microservice.Exceptions
{
    public class InitFailedException: Exception
    {
        public InitFailedException(string message) : this(message, null)
        {
        }

        public InitFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

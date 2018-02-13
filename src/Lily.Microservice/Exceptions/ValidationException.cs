using System;

namespace Lily.Microservice.Exceptions
{
    public class ValidationException:Exception
    {
        public string MemberName { get; private set; }
        public ValidationException(string membername, string message) : base(message)
        {
            MemberName = membername;
        }
    }
}

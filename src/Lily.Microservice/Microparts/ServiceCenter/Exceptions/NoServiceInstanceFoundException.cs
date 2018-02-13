using System;

namespace Lily.Microservice.Microparts.ServiceCenter.Exceptions
{
    public class NoServiceInstanceFoundException : Exception
    {
        public NoServiceInstanceFoundException(string serviceName, Exception innerException)
            : base($"Unable to find an instance of the service {serviceName}", innerException) => ServiceName = serviceName;
        public NoServiceInstanceFoundException(string serviceName) : this(serviceName, null) { }
        public string ServiceName { get; }
        public override string ToString() => Message;
    }
}

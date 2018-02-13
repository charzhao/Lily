using System.Net.Http;

namespace Lily.Microservice.Microparts.CallChain.Features
{
    public abstract class TracingHttpDelegatingHandle:DelegatingHandler
    {
        protected readonly string ServiceName;
        protected TracingHttpDelegatingHandle(
            string serviceName, 
            HttpMessageHandler httpMessageHandler = null)
        {
            ServiceName = serviceName;
            InnerHandler = httpMessageHandler ?? new HttpClientHandler();
        }
    }
}

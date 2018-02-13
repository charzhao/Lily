using System.Net.Http;
using System.Threading.Tasks;
using Lily.Microservice.Microparts.CallChain.Features;
using zipkin4net;
using zipkin4net.Transport;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp
{
    internal class TracingHttpDelegatingHandleOfZipkin: TracingHttpDelegatingHandle
    {
        private readonly ITraceInjector _injector;
        public TracingHttpDelegatingHandleOfZipkin(
            string serviceName, HttpMessageHandler httpMessageHandler = null)
            : this(new ZipkinHttpTraceInjector(), serviceName, httpMessageHandler)
        {

        }

        internal TracingHttpDelegatingHandleOfZipkin(
            ITraceInjector injector, string serviceName, 
            HttpMessageHandler httpMessageHandler = null)
            :base(serviceName,httpMessageHandler)
        {
            _injector = injector;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            using (var clientTrace = new ClientTrace(ServiceName, request.Method.ToString()))
            {
                if (clientTrace.Trace != null)
                {
                    _injector.Inject(clientTrace.Trace, request.Headers, (c, key, value) => c.Add(key, value));
                }
                return await clientTrace.TracedActionAsync(base.SendAsync(request, cancellationToken));
            }
        }
    }
}

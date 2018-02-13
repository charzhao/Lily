using Lily.Microservice.Microparts.CallChain.Features;
using Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp;
using Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.CallChain.Zipkin
{
    public static class MicropartServiceCollectionExtensions
    {
        public static IMicropartServiceCollection AddCallTreeOfZipkin(this IMicropartServiceCollection micropartServiceCollection)
        {
            var serviceCollection = micropartServiceCollection.ApplicationServiceCollection;
            serviceCollection.AddSingleton<IConsumerCallTree, ConsumerCallTree>();
            serviceCollection.AddSingleton<IOperationCallTree, OperationCallTree>();
            serviceCollection.AddSingleton<IProducerCallTree, ProducerCallTree>();

            serviceCollection.AddSingleton<TracingHttpDelegatingHandle, TracingHttpDelegatingHandleOfZipkin>();
            serviceCollection.AddSingleton<IMicropartOfCallTreeStartup, MicropartOfZipkinStartup>();
            serviceCollection.AddSingleton<IMicropartOfCallTree, MicroPartOfZipkin>();
            serviceCollection.AddSingleton<IMicroPartInfo, MicropartInfoOfZipkin>();
            return micropartServiceCollection;
        }
    }
}

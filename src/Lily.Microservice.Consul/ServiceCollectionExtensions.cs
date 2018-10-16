using Lily.Microservice.Consul.InterfaceImp;
using Lily.Microservice.Microparts.ConfigurationCenter;
using Lily.Microservice.Microparts.ServiceCenter;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Consul
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsul(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMicropartOfServiceCenterStartup,MicropartOfConsulStartup>();
            serviceCollection.AddTransient<IMicropartOfConfiguratonCenterStartup, MicropartOfConsulStartup>();
            return serviceCollection;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Consul
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsul(
            this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}

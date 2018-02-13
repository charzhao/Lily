using Lily.Microservice.Microparts.EventBus.RabbitMq.InterfaceImp;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq
{
    public static class MicropartServiceCollectionExtensions
    {
        public static IMicropartServiceCollection AddRabbitMqAsEventBus(this IMicropartServiceCollection micropartServiceCollection)
        {
            var serviceCollection = micropartServiceCollection.ApplicationServiceCollection;
            serviceCollection.AddSingleton<IMicroPartInfo, MicropartInfoOfEventBus>();
            serviceCollection.AddSingleton<IMicropartOfEventBus, MicropartOfEventBus>();
            serviceCollection.AddSingleton<IMicropartOfEventBusStartup, MicropartOfEventBusStartup>();
            return micropartServiceCollection;
        }
    }
}

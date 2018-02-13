using System;
using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.AppConfigurtion.Imp;
using Lily.Microservice.AppLifetime;
using Lily.Microservice.AppTrace;
using Lily.Microservice.AppTrace.Imp;
using Lily.Microservice.Microparts;
using Lily.Microservice.Microparts.Imp;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddMicroservie(this IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddSingleton<IMicroserviceBuilder, MicroserviceBuilder>();
        //    return serviceCollection;
        //}

        public static IServiceCollection AddMicroservie(
            this IServiceCollection serviceCollection,
            Action<IMicropartServiceCollection> microserviceCollectionAction)
        {
            microserviceCollectionAction(new MicropartServiceCollection(serviceCollection));

            serviceCollection.AddSingleton<IAppLifetime, AppLifetime.Imp.AppLifetime>();
            serviceCollection.AddSingleton<IMicroserviceBuilder, MicroserviceBuilder>();

            serviceCollection.AddSingleton<IAppConfigurationManager, AppConfigurationManager>();
            

            serviceCollection.AddSingleton<IMicropartStatusManager, MicropartStatusManager>();
            serviceCollection.AddSingleton<IMicroserviceTraceInfoManager, MicroserviceTraceInfoManager>();
            return serviceCollection;
        }
    }
}

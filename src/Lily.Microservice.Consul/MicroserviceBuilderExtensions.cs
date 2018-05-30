using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.AppLifetime;
using Lily.Microservice.Consul.Config;
using Lily.Microservice.Microparts.ServiceCenter;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Consul
{
    public static class MicroserviceBuilderExtensions
    {
        public static IMicroserviceBuilder UseConsul(
            this IMicroserviceBuilder microserviceBuilder
        )
        {
            var serverCenterStartup = microserviceBuilder.ApplicationServices.GetService<IMicropartOfServiceCenterStartup>();
            var lifetime = microserviceBuilder.ApplicationServices.GetService<IAppLifetime>();
            lifetime.ApplicationStarted.Register(() => serverCenterStartup.Start());
            lifetime.ApplicationStopped.Register(serverCenterStartup.Stop);

            var appConfigurationManager = microserviceBuilder.ApplicationServices.GetService<IAppConfigurationManager>();
            appConfigurationManager.MonitorConfig<AppConfigOfConsul>();
            return microserviceBuilder;
        }
    }
}

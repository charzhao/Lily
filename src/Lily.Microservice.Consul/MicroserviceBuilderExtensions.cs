using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.Consul.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Consul
{
    public static class MicroserviceBuilderExtensions
    {
        public static IMicroserviceBuilder UseConsul(
            this IMicroserviceBuilder microserviceBuilder
        )
        {
            var appConfigurationManager = microserviceBuilder.ApplicationServices.GetService<IAppConfigurationManager>();
            appConfigurationManager.MonitorConfig<AppConfigOfConsul>();
            return microserviceBuilder;
        }
    }
}

using System;
using System.IO;
using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.AppInfo;
using Lily.Microservice.Microparts;
using Lily.Microservice.Microparts.HealthCheck.Config;
using Lily.Microservice.Microparts.Imp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice
{
    public class MicroserviceBuilder: IMicroserviceBuilder
    {
        const string MicroServiceKey = "Service";
        public IServiceProvider ApplicationServices { get; set; }
        private static IServiceProvider _microserviceBuilder;
        public MicroserviceBuilder(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
            _microserviceBuilder = serviceProvider;
        }

        public IMicroserviceBuilder UseMicroservice(Action<IMicropartBuilder> micropartBuilderAction)
        {
            var appConfigurationManager = ApplicationServices.GetService<IAppConfigurationManager>();
            appConfigurationManager.MonitorConfig<AppConfigOfHealthCheck>();

            micropartBuilderAction(new MicropartBuilder(ApplicationServices));
            return this;
        }

        public static void Init()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("lily.microservice.serviceinfo.json", false, true).Build();

            configuration.GetSection(MicroServiceKey).Bind(AppInfoProvider.Service);
        }

        public static T GetService<T>()
        {
            if (_microserviceBuilder == null)
            {
                throw new ArgumentException("system did not init,please init the system first");
            }

          return  _microserviceBuilder.GetService<T>();
        }
    }
}

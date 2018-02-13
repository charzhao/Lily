using System;
using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.AppLifetime;
using Lily.Microservice.Microparts.CallChain.Zipkin.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.CallChain.Zipkin
{
    public static class MicropartBuilderExtensions
    {
        public static IMicropartBuilder UseCallTraceOfZipkin(this IMicropartBuilder app)
        {
            var appConfigurationManager = app.ApplicationServices.GetService<IAppConfigurationManager>();
            appConfigurationManager.MonitorConfig<AppConfigOfZipkin>();

            var micropartOfCallTreeStartup = app.ApplicationServices.GetService<IMicropartOfCallTreeStartup>();
                
            var lifetime = app.ApplicationServices.GetService<IAppLifetime>();
            lifetime.ApplicationStarted.Register(() => micropartOfCallTreeStartup.Start());
            lifetime.ApplicationStopped.Register(micropartOfCallTreeStartup.Stop);
            return app;
        }

        public static IMicropartBuilder UseCallTraceOfZipkin(this IMicropartBuilder micropartBuilder,Action<MicropartOfCallChainAsZipkinBuilder> micropartOfCallChainAsZipkinBuilder)
        {
            micropartBuilder.UseCallTraceOfZipkin();
            micropartOfCallChainAsZipkinBuilder(new MicropartOfCallChainAsZipkinBuilder());
            return micropartBuilder;
        }
    }
}

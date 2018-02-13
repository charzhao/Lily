using Lily.Microservice.AppTrace;
using Lily.Microservice.Microparts.Log.Nlog.Imp.TraceInfo;
using Lily.Microservice.Microparts.Log.Nlog.InterfaceImp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;

namespace Lily.Microservice.Microparts.Log.Nlog
{
    public static class MicropartBuilderExtensions
    {
        public static IMicropartBuilder UseLogOfNLog(this IMicropartBuilder app)
        {
            var microServiceStatusManager = app.ApplicationServices.GetService<IMicropartStatusManager>();
            var micropartInfoOfNLog = microServiceStatusManager.UpdateStatus<MicropartInfoOfNLog>(MicropartType.Log);
            if (micropartInfoOfNLog.IsEnabled)
            {
                var microserviceTraceInfoManager = app.ApplicationServices.GetService<IMicroserviceTraceInfoManager>();
                TraceInfoLayoutRender.GetTraceInfo = () => microserviceTraceInfoManager.GetCurrentTraceInfo();

                var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
                loggerFactory.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });

                ConfigurationItemFactory
                    .Default
                    .LayoutRenderers
                    .RegisterDefinition("traceInfo", typeof(TraceInfoLayoutRender));

                loggerFactory.ConfigureNLog("nlog.config");
            }

            return app;

        }
    }
}

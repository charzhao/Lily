using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.Microparts.CallChain.Zipkin.Config;
using Lily.Microservice.Microparts.CallChain.Zipkin.Imp.Common;
using Microsoft.Extensions.Logging;
using zipkin4net;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp
{
    internal class MicropartOfZipkinStartup : IMicropartOfCallTreeStartup
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IAppConfigurationManager _appConfigurationManager;
        public MicropartOfZipkinStartup(
            ILoggerFactory loggerFactory,
            IAppConfigurationManager appConfigurationManager)
        {
            _loggerFactory = loggerFactory;
            _appConfigurationManager = appConfigurationManager;
        }
        public void Start()
        {
            var configOfZipkin = _appConfigurationManager.GetConfig<AppConfigOfZipkin>();
            TraceManager.SamplingRate = 1.0f;
            _loggerFactory.AddConsole();
            var logger = new TracingLogger(_loggerFactory, "zipkin4net");
            var httpSender = new HttpZipkinSender(configOfZipkin.Address, "application/json");
            var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());
            TraceManager.RegisterTracer(tracer);
            TraceManager.Start(logger);
        }

        public void Stop()
        {
            TraceManager.Stop();
        }
    }
}

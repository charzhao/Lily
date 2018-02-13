using Lily.Microservice.Microparts.Log.Nlog.InterfaceImp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Microparts.Log.Nlog
{
    public static class MicropartServiceCollectionExtensions
    {
        public static IMicropartServiceCollection AddLogOfNLog(this IMicropartServiceCollection micropartServiceCollection)
        {
            var serviceCollection = micropartServiceCollection.ApplicationServiceCollection;

            serviceCollection.AddLogging();

            serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
            serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            serviceCollection.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

            serviceCollection.AddSingleton<IMicroPartInfo, MicropartInfoOfNLog>();
            return micropartServiceCollection;
        }
    }
}

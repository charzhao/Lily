using Lily.Microservice.AppLifetime;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq
{
    public static class MicropartBuilderExtensions
    {
        public static IMicropartBuilder UseRabbitMqAsEventBus(this IMicropartBuilder app)
        {
            var micropartOfEventBusStartup = app.ApplicationServices.GetService<IMicropartOfEventBusStartup>();

            var lifetime = app.ApplicationServices.GetService<IAppLifetime>();
            lifetime.ApplicationStarted.Register(() => micropartOfEventBusStartup.Start());
            lifetime.ApplicationStopped.Register(micropartOfEventBusStartup.Stop);
            return app;
        }
    }
}

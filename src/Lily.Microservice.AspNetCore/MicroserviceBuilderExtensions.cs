using Lily.Microservice.AppLifetime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.AspNetCore
{
    public static class MicroserviceBuilderExtensions
    {
        public static IMicroserviceBuilder UseMicroservieOfAspNetCore(
            this IMicroserviceBuilder microserviceBuilder, 
            IApplicationBuilder app
        )
        {
            var applicationLfetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            var lifetime = app.ApplicationServices.GetService<IAppLifetime>();

            applicationLfetime.ApplicationStarted.Register(() => lifetime.StartApplication());
            applicationLfetime.ApplicationStopped.Register(lifetime.StopApplication);
            return microserviceBuilder;
        }
    }
}

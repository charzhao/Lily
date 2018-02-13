using Lily.Microservice.AppTrace;
using Lily.Microservice.AspNetCore.Imp;
using Lily.Microservice.Microparts.HealthCheck.AspNetCore.Imp;
using Lily.Microservice.Microparts.HealthCheck.Features.HttpHealthCheck;
using Lily.Microservice.Microparts.ServiceCenter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.HealthCheck.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IMicropartBuilder UseHttpHealthCheckOfAspNetCore(this IMicropartBuilder micropartBuilder, IApplicationBuilder app)
        {
            var httpHealtheCheck = new HttpHealthCheck();
            app.Map(httpHealtheCheck.HealthCheckUrl, 
                builder => builder.Run(async context =>
                {
                    await context.Response.WriteAsync("OK1");
                }));
            app.ApplicationServices.GetService<IMicropartOfServiceCenter>().RegisterService();
            var httpRequestFilter=app.ApplicationServices.GetService<IMicroserviceFilterManager<HttpRequestFilter,HttpRequest>>();
            httpRequestFilter.AddFilter(new HttpRequestFilterOfHealthCheck());
            return micropartBuilder;
        }
    }
}

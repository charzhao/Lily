using Lily.Microservice.AppTrace;
using Lily.Microservice.AppTrace.Imp;
using Lily.Microservice.AspNetCore.Imp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMicroservieOfAspNetCore(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<
                IMicroserviceFilterManager<HttpRequestFilter, HttpRequest>,
                MicroserviceFilterManager<HttpRequestFilter, HttpRequest>>();
            return serviceCollection;
        }
    }
}

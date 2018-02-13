namespace Lily.Microservice.Microparts.HealthCheck.AspNetCore
{
    public static class MicropartServiceCollectionExtensions
    {
        public static IMicropartServiceCollection AddHttpHealthCheckOfAspNetCore(this IMicropartServiceCollection micropartServiceCollection)
        {
            var serviceCollection = micropartServiceCollection.ApplicationServiceCollection;
            return micropartServiceCollection;
        }

    }
}

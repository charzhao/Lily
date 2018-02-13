using Lily.Microservice.Microparts.ConfigurationCenter.Consul.InterfaceImp;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.ConfigurationCenter.Consul
{
    public static class MicropartServiceCollectionExtensions
    {
        /// <summary>
        /// inject configurationCenter
        /// </summary>
        /// <param name="micropartServiceCollection"></param>
        /// <returns></returns>
        public static IMicropartServiceCollection AddConfigurationCenterOfConsul(this IMicropartServiceCollection micropartServiceCollection)
        {
            var serviceCollection = micropartServiceCollection.ApplicationServiceCollection;
            serviceCollection.AddSingleton<IMicropartOfConfigurationCenter, MicropartOfConfigurationCenter>();
            serviceCollection.AddSingleton<IMicroPartInfo, MicropartInfoOfConfigurationCenter>();
            return micropartServiceCollection;
        }

    }
}

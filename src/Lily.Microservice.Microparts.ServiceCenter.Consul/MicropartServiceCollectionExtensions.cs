using Lily.Microservice.Microparts.ServiceCenter.Consul.InterfaceImp;
using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.ServiceCenter.Consul
{
    public static class MicropartServiceCollectionExtensions
    {
        public static IMicropartServiceCollection AddServiceCenterOfConsul(this IMicropartServiceCollection micropartServiceCollection)
        {
            var serviceCollection = micropartServiceCollection.ApplicationServiceCollection;
            serviceCollection.AddSingleton<IMicropartOfServiceCenter, MicropartOfServiceCenter>();          
            serviceCollection.AddSingleton<IMicroPartInfo, MicropartInfoOfServiceCenter>();       
            return micropartServiceCollection;
        }

    }
}

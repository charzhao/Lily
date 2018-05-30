using Lily.Microservice.Common.Extensions;
using Lily.Microservice.Consul.Imp.Provider;
using Lily.Microservice.Consul.Imp.Setting;
using Lily.Microservice.Microparts.ConfigurationCenter;
using Lily.Microservice.Microparts.ServiceCenter;

namespace Lily.Microservice.Consul.InterfaceImp
{
    public class MicropartOfConsulStartup : 
        IMicropartOfConfiguratonCenterStartup, 
        IMicropartOfServiceCenterStartup
    {
        private ConsulSetting _consulConfig = SingletonUtil.Singleton<ConsulSetting>.Instance;
        private ConsulClientProvider _client = SingletonUtil.Singleton<ConsulClientProvider>.Instance;
        public void Start()
        {
        }

        public void Stop()
        {

        }
    }
}

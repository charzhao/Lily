using System;
using Consul;
using Lily.Microservice.Common.Extensions;
using Lily.Microservice.Consul.Imp.Setting;

namespace Lily.Microservice.Consul.Imp.Provider
{
    public class ConsulClientProvider
    {
        private readonly ConsulClient _client;
        public ConsulClientProvider()
        {
            var consulConfig = SingletonUtil.Singleton<ConsulSetting>.Instance;
            var agentConfig = consulConfig.Agent;
            Action<ConsulClientConfiguration> configOverride = null;
            if (agentConfig != null)
            {
                if (string.IsNullOrWhiteSpace(agentConfig.Address))
                    throw new ArgumentNullException("agentAddress");
                if (agentConfig.Port == 0)
                    throw new ArgumentNullException("agentPort");
                configOverride = (config) =>
                {
                    config.Address = new UriBuilder($"{agentConfig.Address}:{agentConfig.Port}").Uri;
                    config.Datacenter = agentConfig.Datacenter;
                };
            }
            _client = new ConsulClient(configOverride);
        }

        public ConsulClient GetConsulClient()
        {
            return _client;
        }
    }
}

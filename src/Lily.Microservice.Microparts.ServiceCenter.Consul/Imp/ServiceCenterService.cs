using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Consul;
using Lily.Microservice.AppInfo.AppInfoModel;
using Lily.Microservice.Common.Extensions;
using Lily.Microservice.Consul.Imp.Provider;
using Lily.Microservice.Consul.Imp.Setting;
using Lily.Microservice.Consul.Imp.Watcher;
using Lily.Microservice.Microparts.ServiceCenter.Routing;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Microparts.ServiceCenter.Consul.Imp
{
    internal class ServiceCenterService
    {
        private readonly ConsulClient _client;
        private readonly ILogger _logger;
        private readonly ConsulSetting _consulConfig;
        public ServiceCenterService(ILogger logger)
        {
            _consulConfig = SingletonUtil.Singleton<ConsulSetting>.Instance;
            _logger = logger;
            _client = SingletonUtil.Singleton<ConsulClientProvider>.Instance.GetConsulClient();
        }

        private readonly List<string> _services = new List<string>();

        public bool ServiceRegister()
        {
            var agentService = _consulConfig.Service.Convert();
            if (!_services.Contains(agentService.Name))
            {
                ServiceRegisterInternal(agentService);
                _services.Add(agentService.Name);
            }
            return true;
        }

        public async Task<ServiceInfo> GetServiceAsync(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentNullException("serviceName");
            }
            var watcherContext = new WatcherContext<IList<ServiceInfo>>(
                () =>
                {
                    return new ServicesMonitorWatcher(
                        serviceName,
                        RoutingStrategyFactory.GetRouting<ServiceInfo>(_consulConfig.Route),
                        _client,
                        _logger);
                });
            var services = await watcherContext.GetInstanceAsync();
            return services[0];
        }

        private void ServiceRegisterInternal(AgentServiceRegistration service)
        {
            var result = _client.Agent.ServiceRegister(service).GetAwaiter().GetResult();
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ConsulRequestException("service register failed", result.StatusCode);
            }
        }
    }
}

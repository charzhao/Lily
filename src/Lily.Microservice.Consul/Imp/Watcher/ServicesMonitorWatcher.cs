using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Lily.Microservice.AppInfo.AppInfoModel;
using Lily.Microservice.Microparts.ServiceCenter.Exceptions;
using Lily.Microservice.Microparts.ServiceCenter.Routing;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Consul.Imp.Watcher
{
    public class ServicesMonitorWatcher : Watcher<IList<ServiceInfo>>
    {
        private readonly IRoutingStrategy<ServiceInfo> _routingStrategy;
        public ServicesMonitorWatcher(string serviceName, IRoutingStrategy<ServiceInfo> routingStrategy, ConsulClient client, ILogger logger) 
            : base(serviceName, client, logger, WatcherType.Services)
        {
            _routingStrategy = routingStrategy;
        }

        public override async Task<IList<ServiceInfo>> GetAsync()
        {
            var services = await base.GetAsync();
            if (services == null || services.Count == 0)
                throw new NoServiceInstanceFoundException(_watcherKey);
            return new List<ServiceInfo> { _routingStrategy.RouteTo(services) };
        }

        protected override async Task<QueryResult<IList<ServiceInfo>>> GetResponseAsync(string watcherKey, QueryOptions queryOptions, CancellationToken cancelationToken)
        {
            var result = await _client.Health.Service(_watcherKey, null, true,
                                queryOptions, cancelationToken);
            return new QueryResult<IList<ServiceInfo>>(result, Convert(result.Response));
        }

        private IList<ServiceInfo> Convert(IList<ServiceEntry> serviceEntries)
        {
            if (serviceEntries != null && serviceEntries.Count > 0)
            {
                var services = new List<ServiceInfo>();
                foreach (var instance in serviceEntries)
                {
                    services.Add(new ServiceInfo(instance.Service.ID)
                    {
                        Address = instance.Service.Address,
                        Port = instance.Service.Port,
                        Name = instance.Service.Service,
                        Tags = instance.Service.Tags
                    });
                }
                return services;
            }
            return null;
        }

        public override string ToString()
        {
            return $"{WatcherType.Services.ToString()}:{_watcherKey}";
        }
    }
}

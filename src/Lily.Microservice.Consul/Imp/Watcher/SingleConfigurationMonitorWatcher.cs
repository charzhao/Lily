using System.Threading;
using System.Threading.Tasks;
using Consul;
using Lily.Microservice.Microparts.ConfigurationCenter.Model;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Consul.Imp.Watcher
{
    public class SingleConfigurationMonitorWatcher: Watcher<KeyValueConfig>
    {
        public SingleConfigurationMonitorWatcher(string watcherKey, ConsulClient client, ILogger logger) : 
            base(watcherKey, client, logger, WatcherType.Key)
        {
        }

        protected override async Task<QueryResult<KeyValueConfig>> GetResponseAsync(string watcherKey, QueryOptions queryOptions, CancellationToken cancelationToken)
        {
            var result = await _client.KV.Get(watcherKey, queryOptions, cancelationToken);
            return new QueryResult<KeyValueConfig>(result, result.Response != null ? new KeyValueConfig(result.Response.Key, result.Response.Value) : default(KeyValueConfig));              
        }
    }
}

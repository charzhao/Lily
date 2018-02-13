using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Lily.Microservice.Microparts.ConfigurationCenter.Model;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Consul.Imp.Watcher
{
    public class ConfigurationMonitorWatcher : Watcher<IList<KeyValueConfig>>
    {
        public ConfigurationMonitorWatcher(string watcherKey, ConsulClient client, ILogger logger) 
            : base(watcherKey, client, logger, WatcherType.Keyprefix)
        {
        }

        protected override async Task<QueryResult<IList<KeyValueConfig>>> GetResponseAsync(string watcherKey, QueryOptions queryOptions, CancellationToken cancelationToken)
        {
            var result = await _client.KV.List(watcherKey, queryOptions, cancelationToken);
            return new QueryResult<IList<KeyValueConfig>>(result, Convert(result.Response));
        }

        private IList<KeyValueConfig> Convert(KVPair[] instances)
        {
            if (instances == null || instances.Length == 0)
                return null;
            var settings = new List<KeyValueConfig>();
            foreach (var kv in instances)
            {
                settings.Add(new KeyValueConfig(kv.Key, kv.Value));
            }
            return settings;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Consul;
using Lily.Microservice.Common.Extensions;
using Lily.Microservice.Consul.Imp.Provider;
using Lily.Microservice.Consul.Imp.Watcher;
using Lily.Microservice.Exceptions;
using Lily.Microservice.Microparts.ConfigurationCenter.Exceptions;
using Lily.Microservice.Microparts.ConfigurationCenter.Model;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Microparts.ConfigurationCenter.Consul.Imp
{
    internal class ConfigurationCenterService
    {
        private readonly ConsulClient _client;
        private readonly ILogger _logger;
        public ConfigurationCenterService(ILogger logger)
        {
            _logger = logger;
            _client = SingletonUtil.Singleton<ConsulClientProvider>.Instance.GetConsulClient();
        }

        public async Task<bool> SetAsync(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException(nameof(value), "value is empty");
            var kv = new KVPair(key) { Value = KeyValueConfig.ConvertValueToByte(value) };
            var result = await _client.KV.Put(kv);
            return result.Response;
        }

        public async Task<KeyValueConfig> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ValidationException(nameof(key), "key is empty");
            var result = await _client.KV.Get(key);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                _logger?.LogWarning($"key:{key} query is not ok, statusCode: {result.StatusCode}");
                return null;
            }
            return new KeyValueConfig(result.Response.Key, result.Response.Value);
        }

        public async Task<IList<KeyValueConfig>> ListAsync(string prefix)
        {
            var result = await _client.KV.List(prefix);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                _logger?.LogWarning($"key:{prefix} list query is not ok, statusCode: {result.StatusCode}");
                return null;
            }
            var settings = new List<KeyValueConfig>();
            foreach (var kv in result.Response)
            {
                settings.Add(new KeyValueConfig(kv.Key, kv.Value));
            }
            return settings;
        }

        public Task<KeyValueConfig> GetAndWatchAsync(string key, Action<WatcherEventArg<KeyValueConfig>> watchCallback)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ValidationException(nameof(key), "key is empty");
            var watcherContext = new WatcherContext<KeyValueConfig>(
                () =>
                {
                    return new SingleConfigurationMonitorWatcher(key, _client, _logger)
                    {
                        WatchChanged = watchCallback
                    };
                });
            return watcherContext.GetInstanceAsync();
        }

        public Task<IList<KeyValueConfig>> ListAndWatchAsync(string prefix, Action<WatcherEventArg<IList<KeyValueConfig>>> watchCallback)
        {
            var watcherContext = new WatcherContext<IList<KeyValueConfig>>(
                () =>
                {
                    return new ConfigurationMonitorWatcher(prefix, _client, _logger)
                    {
                        WatchChanged = watchCallback
                    };
                });
            return watcherContext.GetInstanceAsync();
        }

        public async Task<bool> DeleteAsync(string key)
        {
            var result = await _client.KV.Delete(key);
            _logger?.LogTrace($"key:{key} was deleted");
            return result.Response;
        }

        public async Task<bool> DeleteTreeAsync(string prefix)
        {
            var result = await _client.KV.DeleteTree(prefix);
            _logger?.LogTrace($"key:{prefix} tree was deleted ");
            return result.Response;
        }

        public async Task SetByTransaction(IList<KeyValueConfig> settings)
        {
            if (settings == null || settings.Count == 0)
                throw new ValidationException(nameof(settings), "settings is empty");
            List<KVTxnOp> txn = new List<KVTxnOp>();
            foreach (var setting in settings)
            {
                txn.Add(new KVTxnOp(setting.Key, KVTxnVerb.Set) { Value = setting.ConvertValueToByte() });
            }
            var result = await _client.KV.Txn(txn);
            if (!result.Response.Success)
            {
                throw new ConfigurateException($"it was rolled back, error reason{result.Response.Errors[0].What}");
            }
        }

    }
}

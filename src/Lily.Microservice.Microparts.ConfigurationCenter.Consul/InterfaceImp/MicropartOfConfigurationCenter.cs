using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lily.Microservice.Exceptions;
using Lily.Microservice.Microparts.ConfigurationCenter.Consul.Imp;
using Lily.Microservice.Microparts.ConfigurationCenter.Model;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Microparts.ConfigurationCenter.Consul.InterfaceImp
{
    internal class MicropartOfConfigurationCenter :BaseMicropartOfConfigurationCenter, IMicropartOfConfigurationCenter
    {
        private readonly ConfigurationCenterService _configurationCenterService;

        public MicropartOfConfigurationCenter(
            ILoggerFactory loggerFactory,
            IMicropartStatusManager micropartStatusManager)
        {
            _configurationCenterService = new ConfigurationCenterService(loggerFactory?.CreateLogger("Consul"));
            var micropartInfoOfConfigurationCenter = micropartStatusManager.UpdateStatus<MicropartInfoOfConfigurationCenter>(MicropartType);
            if (!micropartInfoOfConfigurationCenter.IsEnabled)
            {
                throw new MicroServicePartNotEnabledException(micropartInfoOfConfigurationCenter.MicroServicePartName);
            }
        }

        //todo:authorization
        public Task<bool> SetAsync(string key, string value)
        {
            return _configurationCenterService.SetAsync(key, value);
        }

        public Task<KeyValueConfig> GetAsync(string key)
        {
            return _configurationCenterService.GetAsync(key);
        }

        public Task<KeyValueConfig> GetAndWatchCacheAsync(string key, Action<WatcherEventArg<KeyValueConfig>> watchCallback = null)
        {
            return _configurationCenterService.GetAndWatchAsync(key, watchCallback);
        }

        public Task<IList<KeyValueConfig>> ListAndWatchCacheAsync(string prefix, Action<WatcherEventArg<IList<KeyValueConfig>>> watchCallback = null)
        {
            return _configurationCenterService.ListAndWatchAsync(prefix, watchCallback);
        }

        //todo:authorization
        public Task<bool> DeleteAsync(string key)
        {
            return _configurationCenterService.DeleteAsync(key);
        }
        //todo:authorization
        public Task<bool> DeleteTreeAsync(string prefix)
        {
            return _configurationCenterService.DeleteTreeAsync(prefix);
        }

        public Task<IList<KeyValueConfig>> ListAsync(string prefix)
        {
            return _configurationCenterService.ListAsync(prefix);
        }

        //todo:authorization
        public Task SetByTransaction(IList<KeyValueConfig> settings)
        {
            return _configurationCenterService.SetByTransaction(settings);
        }
    }
}

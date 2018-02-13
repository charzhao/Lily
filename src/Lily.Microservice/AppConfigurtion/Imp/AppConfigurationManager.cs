using System.Collections.Generic;
using Lily.Microservice.AppConfigurtion.ConfigurationExtensions;
using Lily.Microservice.AppConfigurtion.Imp.Model;
using Lily.Microservice.Common.Extensions;
using Lily.Microservice.Microparts.ConfigurationCenter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lily.Microservice.AppConfigurtion.Imp
{
    public class AppConfigurationManager : IAppConfigurationManager
    {
        private readonly ILogger _logger;
        private readonly IMicropartOfConfigurationCenter _micropartOfConfigurationCenter;
        private readonly IConfigurationBuilder _configurationBuilder = new ConfigurationBuilder();
        private bool _isConfigurationBuilderUpdated = true;


        public AppConfigurationManager(
            ILoggerFactory loggerFactory,
            IMicropartOfConfigurationCenter micropartOfConfigurationCenter
            )
        {
            _logger = loggerFactory.CreateLogger(nameof(AppConfigurationManager));
            _micropartOfConfigurationCenter = micropartOfConfigurationCenter;
        }


        public T GetConfig<T>() where T:IAppConfig
        {
            var appconfigurationRoot = GetAppConfigurationRoot();
            KeyEntry<T> keyEntry = SingletonUtil.Singleton<KeyEntry<T>>.Instance;
            var data = new Dictionary<string, string>();
            foreach (var metaData in keyEntry.MetaDatas)
            {
                string value = appconfigurationRoot[metaData.Value.KeyTemplate];
                if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(metaData.Value.DefaultKeyTemplate))
                {
                    value = appconfigurationRoot[metaData.Value.DefaultKeyTemplate];
                    if (string.IsNullOrWhiteSpace(value))
                        continue;
                }
                data[metaData.Key.Name] = value;
            }
            if (data.Count == 0)
                return default(T);
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));
        }

        public void MonitorConfig<T>() where T : IAppConfig
        {
            _isConfigurationBuilderUpdated = true;
            _configurationBuilder.Add(new ConfigurationCenterRegistrySource<T>(_micropartOfConfigurationCenter));
        }

        private IConfigurationRoot _configurationRoot;
        private IConfigurationRoot GetAppConfigurationRoot()
        {
            if (_isConfigurationBuilderUpdated)
            {
                _configurationRoot = _configurationBuilder.Build();
            }

            return _configurationRoot;
        }
    }
}

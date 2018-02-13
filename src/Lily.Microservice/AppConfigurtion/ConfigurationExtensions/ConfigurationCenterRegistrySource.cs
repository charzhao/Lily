using Lily.Microservice.Microparts.ConfigurationCenter;
using Microsoft.Extensions.Configuration;

namespace Lily.Microservice.AppConfigurtion.ConfigurationExtensions
{
    internal class ConfigurationCenterRegistrySource<T> : IConfigurationSource
    {
        private readonly IMicropartOfConfigurationCenter _configurationCenterRegistry;

        public ConfigurationCenterRegistrySource(IMicropartOfConfigurationCenter configurationCenterRegistry) => _configurationCenterRegistry = configurationCenterRegistry;

        public IConfigurationProvider Build(IConfigurationBuilder builder) => new ConfigurationCenterRegistryProvider<T>(_configurationCenterRegistry);
    }
}
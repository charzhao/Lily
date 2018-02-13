using System.IO;
using Lily.Microservice.AppInfo;
using Lily.Microservice.Consul.Imp.Setting.SettingItems;
using Lily.Microservice.Microparts.ServiceCenter.Routing;
using Microsoft.Extensions.Configuration;

namespace Lily.Microservice.Consul.Imp.Setting
{
    public class ConsulSetting
    {
        const string ConsulKey = "Consul";
        private static readonly IConfiguration Configuration;
        static ConsulSetting()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("lily.microservice.consul.json", true, true)
                .Build();
        }

        public ConsulSetting()
        {
            Configuration
                .GetSection(ConsulKey)
                .Bind(this);
        }

        public AgentSetting Agent { get; set; }

        public ServiceSetting Service { get; set; } = new ServiceSetting();

        public RouteStrategy Route { get; set; }
    }
}

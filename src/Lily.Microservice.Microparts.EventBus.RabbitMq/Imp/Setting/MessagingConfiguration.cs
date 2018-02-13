using Microsoft.Extensions.Configuration;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Setting
{
    internal class MessagingConfiguration
    {
        public static MessagingConfiguration Instance = new MessagingConfiguration();

        private const string MessageConfigurationKey = "MessageConfiguration";
        
        private MessagingConfiguration()
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory;
            var configuration = new ConfigurationBuilder().SetBasePath(path)
                  .AddJsonFile("microservice.RabbitMQ.json", true, true).Build();
            configuration.GetSection(MessageConfigurationKey).Bind(this);
        }

        public string IspEventStubAssemblyName { get; set; }
        public string IspEventDefaultTypeFullName { get; set; }
        public string EasynetqConnectionString { get; set; }
        public string IspMessageEndPoint { get; set; }
        public string ServiceRuleName { get; set; }
        public bool SubscribeAllEvents { get; set; }

    }
}

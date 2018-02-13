namespace Lily.Microservice.Consul.Imp.Setting.SettingItems
{
    public class AgentSetting
    {
        public string Address { get; set; } = "localhost";

        public int Port { get; set; } = 8500;

        public string Datacenter { get; set; }
    }
}

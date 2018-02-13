namespace Lily.Microservice.Consul.Imp.Setting.SettingItems
{
    public class HealthCheckSetting
    {
        public int? Interval { get; set; }

        public int? Timeout { get; set; }

        public int? TTL { get; set; }

        public string HTTP { get; set; }

        public string ProtocolScheme { get; set; }

        public string TCP { get; set; }

        public bool TLSSkipVerify { get; set; }

        public int? DeregisterCriticalServiceAfter { get; set; }
    }
}

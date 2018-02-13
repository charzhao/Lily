using Newtonsoft.Json;

namespace Tools.Lily.Microservice.ConsulConfiguration.ConsulKeyValueFormat
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class KeyValuePair
    {
        [JsonConverter(typeof(KeyValuePairKeyConverter))]
        public string Key { get; set; }
        [JsonConverter(typeof(KeyValuePairValueConverter))]
        public string Value { get; set; }
    }
}

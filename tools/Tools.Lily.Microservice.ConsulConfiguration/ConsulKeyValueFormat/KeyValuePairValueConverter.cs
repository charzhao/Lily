using System;
using System.Text;
using Newtonsoft.Json;

namespace Tools.Lily.Microservice.ConsulConfiguration.ConsulKeyValueFormat
{
    public class KeyValuePairValueConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                throw new Exception("value is null");
            serializer.Serialize(writer,Convert.ToBase64String(Encoding.UTF8.GetBytes(value.ToString())));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, typeof(string));
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(TimeSpan))
            {
                return true;
            }
            return false;
        }
    }
}
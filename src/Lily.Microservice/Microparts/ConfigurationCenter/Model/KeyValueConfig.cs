using System.Text;
using Lily.Microservice.Exceptions;

namespace Lily.Microservice.Microparts.ConfigurationCenter.Model
{
    public class KeyValueConfig
    {
        public KeyValueConfig(string key, byte[] value)
        {
            Key = key;
            if (value != null)
            {
                _value = Encoding.UTF8.GetString(value);
            }
        }

        public KeyValueConfig(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        private string _value;
        public string Value
        {
            get { return this._value; }
            set {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ValidationException(nameof(Value), "value is null");
                this._value = value;
            }
        }

        public byte[] ConvertValueToByte()
        {
            return ConvertValueToByte(Value);
        }

        public static byte[] ConvertValueToByte(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}

using System;

namespace Lily.Microservice.Microparts.ConfigurationCenter.Exceptions
{
    public class ConfigurateException:Exception
    {
        public ConfigurateException(string message) : base(message)
        {
        }
    }
}

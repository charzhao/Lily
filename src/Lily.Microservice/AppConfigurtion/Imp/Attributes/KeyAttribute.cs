using System;
using Lily.Microservice.AppInfo;
using Lily.Microservice.Exceptions;

namespace Lily.Microservice.AppConfigurtion.Imp.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class KeyAttribute : Attribute
    {
        public KeyAttribute(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ValidationException(nameof(KeyAttribute), "key is empty");
            _key = key.Trim('/');
        }

        private readonly string _key;
        /// <summary>
        /// key match case
        /// </summary>
        public string Key
        {
            get
            {
                if (IsAllPath && !string.IsNullOrWhiteSpace(Root))
                {
                    return $"{Root}/{_key.TrimStart('/')}";
                }
                return _key;
            }
        }

        public bool IsAllPath { get; set; }

        public string Root { get; set; } = AppInfoProvider.AppConfigDefaultkey;

        public string Description { get; set; }
    }
}

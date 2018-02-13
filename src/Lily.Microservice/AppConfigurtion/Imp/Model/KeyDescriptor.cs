using System;
using System.Reflection;
using Lily.Microservice.AppConfigurtion.Imp.Attributes;

namespace Lily.Microservice.AppConfigurtion.Imp.Model
{
    internal class KeyDescriptor
    {

        private event Action<string, string> _watchCallBack;

        private string _name;
        public string Name => _name;

        private string _keyTemplate;
        public string KeyTemplate => _keyTemplate;

        private string _defaultKeyTemplate;
        public string DefaultKeyTemplate => _defaultKeyTemplate;

        public KeyDescriptor(PropertyInfo keyPropertyInfo, string root, string defaultRoot)
        {
            Init(keyPropertyInfo, root, defaultRoot);
        }

        public void Watch(string value)
        {
            _watchCallBack?.Invoke(_name, value);
        }

        private void Init(PropertyInfo keyPropertyInfo, string root, string defaultRoot)
        {
            _name = keyPropertyInfo.Name;
            var watcherAttributes = keyPropertyInfo.GetCustomAttributes<WatcherAttribute>();
            foreach (var watcher in watcherAttributes)
            {
                _watchCallBack += watcher.WatchCallBack;
            }
            var key = keyPropertyInfo.Name;
            var keyAttribute = keyPropertyInfo.GetCustomAttribute<KeyAttribute>();
            if (keyAttribute != null)
            {
                key = keyAttribute.Key;
                if (keyAttribute.IsAllPath)
                {
                    _keyTemplate = key;
                    return;
                }                 
            }
            _keyTemplate = $"{root}/{key}";
            if (!string.IsNullOrWhiteSpace(defaultRoot))
            {
                _defaultKeyTemplate = $"{defaultRoot}/{key}";
            }
        }
    }
}

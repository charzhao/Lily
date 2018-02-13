using System;
using System.Collections.Generic;
using System.Reflection;
using Lily.Microservice.AppConfigurtion.Imp.Attributes;

namespace Lily.Microservice.AppConfigurtion.Imp.Model
{
    internal class KeyEntry<T>
    {
        public KeyEntry()
        {
            Init();
        }

        private Dictionary<PropertyInfo, KeyDescriptor> _metaDatas = new Dictionary<PropertyInfo, KeyDescriptor>();
        public Dictionary<PropertyInfo, KeyDescriptor> MetaDatas => _metaDatas;

        private string _root;
        public string Root => _root;

        private string _defaultRoot;
        public string DefaultRoot => _defaultRoot;

        private string _fileName;
        public string FileName => _fileName;

        private void Init()
        {
            Type type = typeof(T);
            var keyBundle = type.GetCustomAttribute<ServiceCanOverrideBundleAttribute>();
            _root = type.Name;
            _fileName = $"appConfig_{_root}.json";
            if (keyBundle != null)
            {
                if(!string.IsNullOrWhiteSpace(keyBundle.ConfigFileName))
                    _fileName = $"{keyBundle.ConfigFileName}.json";
                _root = keyBundle.ServiceName;
                _defaultRoot = keyBundle.DefaultPath;
            }
            var keyProperties = type.GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            foreach (var property in keyProperties)
            {
                _metaDatas[property] = new KeyDescriptor(property, _root, _defaultRoot);
            }
        }
    }
}

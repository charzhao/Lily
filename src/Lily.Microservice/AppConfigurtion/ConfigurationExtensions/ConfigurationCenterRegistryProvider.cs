using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lily.Microservice.AppConfigurtion.Imp.Model;
using Lily.Microservice.Common.Extensions;
using Lily.Microservice.Common.Helper;
using Lily.Microservice.Microparts.ConfigurationCenter;
using Lily.Microservice.Microparts.ConfigurationCenter.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Lily.Microservice.AppConfigurtion.ConfigurationExtensions
{
    internal class ConfigurationCenterRegistryProvider<T> : ConfigurationProvider
    {
        private static readonly object LockObj = new object();
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        private readonly Dictionary<string, Task<KeyValueConfig>> _tasks = new Dictionary<string, Task<KeyValueConfig>>();
        private readonly IMicropartOfConfigurationCenter _configurationCenterRegistry;
        private readonly KeyEntry<T> _keyEntry;
        private readonly string _fileName;

        public ConfigurationCenterRegistryProvider(IMicropartOfConfigurationCenter configurationCenterRegistry)
        {
            _configurationCenterRegistry = configurationCenterRegistry;
            Data = new Dictionary<string, string>();
            _keyEntry = SingletonUtil.Singleton<KeyEntry<T>>.Instance;
            _fileName = Path.Combine(AppPath.GetAppConfigurationPath(), _keyEntry.FileName);
        }

        public override bool TryGet(string key, out string value)
        {
            if (!Data.TryGetValue(key, out value))
            {
                if (!_tasks.TryGetValue(key, out Task<KeyValueConfig> task))
                    return false;
                var setting = task.GetAwaiter().GetResult();
                if (setting == null)
                    return false;
                value = setting.Value;
            }
            return true;
        }

        public override void Load()
        {
            //get data from local file
            GetFromLocalFile();
            foreach (var metaDate in _keyEntry.MetaDatas)
            {
                var keyDescriptor = metaDate.Value;                  
                _tasks[keyDescriptor.KeyTemplate]=_configurationCenterRegistry.GetAndWatchCacheAsync(keyDescriptor.KeyTemplate, WatchCallBack);
                if (!string.IsNullOrWhiteSpace(keyDescriptor.DefaultKeyTemplate))
                {
                    _tasks[keyDescriptor.DefaultKeyTemplate]=_configurationCenterRegistry.GetAndWatchCacheAsync(keyDescriptor.DefaultKeyTemplate,WatchDefaultKeyCallBack);                 
                }            
            }
        }

        private void WatchCallBack(WatcherEventArg<KeyValueConfig> watcherEventArg)
        {
            KeyDescriptor keyDescriptor = _keyEntry.MetaDatas.Values.FirstOrDefault(k => string.Compare(k.KeyTemplate, watcherEventArg.WatchKey, true) == 0);
            if (keyDescriptor == null)
                return;
            var watchValue = WatchCallBackInternal(watcherEventArg.WatchValue, keyDescriptor.KeyTemplate);
            keyDescriptor.Watch(watchValue);
        }

        private void WatchDefaultKeyCallBack(WatcherEventArg<KeyValueConfig> watcherEventArg)
        {
            KeyDescriptor keyDescriptor = _keyEntry.MetaDatas.Values.FirstOrDefault(k => String.Compare(k.DefaultKeyTemplate, watcherEventArg.WatchKey, StringComparison.OrdinalIgnoreCase) == 0);
            if (keyDescriptor == null)
                return;
             var watchValue = WatchCallBackInternal(watcherEventArg.WatchValue,  keyDescriptor.DefaultKeyTemplate);
            //if not exist KeyTemplate, invoke
            if (Data.ContainsKey(keyDescriptor.KeyTemplate))
                return;
            keyDescriptor.Watch(watchValue);
        }

        private string WatchCallBackInternal(KeyValueConfig setting, string key)
        {
            string watchValue = string.Empty;
            if (setting == null || string.IsNullOrWhiteSpace(setting.Value))
            {
                Data.Remove(key);
            }
            else
            {
                Data[setting.Key] = setting.Value;
                watchValue = setting.Value;
            }
            OnReload();
            lock (LockObj)
            {
                SetToLocalFile();
            }
            return watchValue;
        }

        private void SetToLocalFile()
        {
            if (Data.Count == 0)
            {
                File.Delete(_fileName);
            }
            else
            {
                using (FileStream fs = new FileStream(_fileName, FileMode.Create))
                {
                    byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Data));
                    fs.Write(data, 0, data.Length);
                }
            }
        }

        private void GetFromLocalFile()
        {
            if (!File.Exists(_fileName))
                return;
            using (FileStream stream = new FileStream(_fileName, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        Data = Serializer.Deserialize<Dictionary<string,string>>(jsonReader);
                    }
                }
            }
        }
    }
}
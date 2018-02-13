using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Lily.Microservice.Consul.Imp.Watcher
{
    public class WatcherContext<T>
        where T:class
    {
        private static readonly ConcurrentDictionary<string, Watcher<T>> _watchers = new ConcurrentDictionary<string, Watcher<T>>(StringComparer.OrdinalIgnoreCase);
        private Watcher<T> _watcher;
        public WatcherContext(Func<Watcher<T>> watcherCreater)
        {
            _watcher = watcherCreater.Invoke();
        }

        public Task<T> GetInstanceAsync()
        {
            return GetWatcher().GetAsync();
        }

        private Watcher<T> GetWatcher()
        {
            var key = _watcher.ToString();
            if (!_watchers.TryGetValue(key, out Watcher<T> watcher) || !watcher.IsEnable)
            {
                watcher = _watcher;
                watcher.Start();
                _watchers[key] = watcher;
            }
            return watcher;
        }
    }
}

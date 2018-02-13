namespace Lily.Microservice.Consul.Imp.Watcher
{
    public enum WatcherType
    {
        /// <summary>
        ///  is used to watch the list of available services
        /// </summary>
        Services,
        /// <summary>
        /// is used to watch a specific key in the KV store
        /// </summary>
        Key,
        /// <summary>
        /// is used to watch a prefix of keys in the KV store
        /// </summary>
        Keyprefix,
    }
}

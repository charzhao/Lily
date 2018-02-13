namespace Lily.Microservice.Consul.Imp.Watcher
{
    public enum WatcherState
    {
        NotInitialized,
        UsingCachedValues,
        UsingLiveValues
    }
}
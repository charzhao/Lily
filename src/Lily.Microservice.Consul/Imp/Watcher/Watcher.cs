using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Lily.Microservice.Exceptions;
using Lily.Microservice.Microparts.ConfigurationCenter.Model;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Consul.Imp.Watcher
{
    public abstract class Watcher<TTarget> : IDisposable
                where TTarget : class
    {
        private readonly static int _reconnectDelay = 1500;
        private readonly static int _getDelay = 3000;

        private readonly CancellationTokenSource _cancelationToken = new CancellationTokenSource();
        private readonly TaskCompletionSource<bool> _completionSource = new TaskCompletionSource<bool>();      
        
        private TTarget _instances;
        private WatcherState _state;
        private WatcherType _watcherType;
        private bool _isEnable;

        protected int _waitMaxMin = 10;               
        protected readonly ILogger _logger;
        protected readonly ConsulClient _client;
        protected readonly string _watcherKey;

        public bool IsEnable => _isEnable;
        public Action<WatcherEventArg<TTarget>> WatchChanged { get; set; }

        protected Watcher(string watcherKey, ConsulClient client, ILogger logger, WatcherType watcherType)
        {
            if (string.IsNullOrWhiteSpace(watcherKey))
                throw new ValidationException(nameof(watcherKey), "watcherKey is empty");
            _watcherType = watcherType;
            _watcherKey = watcherKey;
            _logger = logger;
            _client = client;          
        }

        public virtual void Dispose() {           
            _cancelationToken.Cancel();
            _isEnable = false;
        }

        public virtual void Start()
        {
            _isEnable = true;
            var ignore = WatcherLoop();
        }

        public virtual async Task<TTarget> GetAsync()
        {
            var instances = Volatile.Read(ref _instances);
            if (instances == null)
            {
                var delayTask = Task.Delay(_getDelay);
                var taskThatFinished = await Task.WhenAny(delayTask, _completionSource.Task).ConfigureAwait(false);
                if (delayTask == taskThatFinished)
                {
                    throw new InitFailedException($"watcher loop for {_watcherKey} uninit");
                }
                instances = Volatile.Read(ref _instances);
            }
            if (_state != WatcherState.UsingLiveValues)
            {
                _logger?.LogWarning($"Using old values for {_watcherKey}");
            }
            return instances;
        }

        public override string ToString()
        {
            return $"{_client.Config.Datacenter}:{_watcherType.ToString()}:{_watcherKey}";
        }

        protected abstract Task<QueryResult<TTarget>> GetResponseAsync(string watcherKey, QueryOptions queryOptions, CancellationToken cancelationToken);

        private async Task WatcherLoop()
        {
            try
            {
                while (_isEnable)
                {
                    try
                    {
                        ulong consulIndex = 0;
                        while (!_cancelationToken.Token.IsCancellationRequested)
                        {
                            //if no change,wait 10 min
                            var result = await GetResponseAsync(_watcherKey,
                                new QueryOptions { WaitIndex = consulIndex, WaitTime = TimeSpan.FromMinutes(_waitMaxMin) }, _cancelationToken.Token).ConfigureAwait(false);

                            if (result.StatusCode != HttpStatusCode.OK)
                            {
                                //if resource not found, this watcher will dispose
                                if (result.StatusCode == HttpStatusCode.NotFound)
                                {
                                    Dispose();
                                }
                                else
                                {
                                    if (_state == WatcherState.UsingLiveValues)
                                    {
                                        _state = WatcherState.UsingCachedValues;
                                    }
                                    await Task.Delay(1000);
                                    continue;
                                }

                            }
                            if (result.LastIndex == consulIndex)
                            {
                                continue;
                            }

                            consulIndex = result.LastIndex;
                            Volatile.Write(ref _instances, result.Response);
                            _state = WatcherState.UsingLiveValues;
                            _completionSource.TrySetResult(true);
                            var watcherEventArg = new WatcherEventArg<TTarget>
                            {
                                WatchKey = _watcherKey,
                                WatchValue = result.Response,
                            };
                            WatchChanged?.Invoke(watcherEventArg);
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogWarning(0, ex, "Error in blocking watcher watching consul");
                    }
                    _state = _state == WatcherState.NotInitialized ? WatcherState.NotInitialized : WatcherState.UsingCachedValues;
                    if (_cancelationToken.Token.IsCancellationRequested)
                    {
                        _logger?.LogInformation("Cancelation requested exiting watcher");
                        return;
                    }
                    await Task.Delay(_reconnectDelay);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(0, ex, "Exception in watcher loop for {_watcherKey}", _watcherKey);
            }
            finally
            {
                _logger?.LogWarning("Exiting watcher loop for {_watcherKey}", _watcherKey);
            }
        }
    }
}

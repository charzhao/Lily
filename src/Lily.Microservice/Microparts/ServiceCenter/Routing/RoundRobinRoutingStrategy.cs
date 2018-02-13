using System.Collections.Generic;
using System.Threading;

namespace Lily.Microservice.Microparts.ServiceCenter.Routing
{
    public class RoundRobinRoutingStrategy<T> : IRoutingStrategy<T>
    {
        private int _index = -1;

        public T RouteTo(IList<T> instances)
        {
            if (instances?.Count > 0)
            {
                var index = Interlocked.Increment(ref _index);
                return instances[index % instances.Count];
            }
            return default(T);
        }

        public RouteStrategy Strategy { get; } = RouteStrategy.RoundRobin;
    }
}

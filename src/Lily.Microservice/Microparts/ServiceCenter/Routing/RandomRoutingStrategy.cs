using System.Collections.Generic;
using Lily.Microservice.Common.Helper;

namespace Lily.Microservice.Microparts.ServiceCenter.Routing
{
    public class RandomRoutingStrategy<T> : IRoutingStrategy<T>
    {
        public T RouteTo(IList<T> instances) => instances?.Count > 0 ? instances[UtilHelper.Next(0, instances.Count)] : default(T);
        public RouteStrategy Strategy { get; } = RouteStrategy.Random;
    }
}

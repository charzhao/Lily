using System.Collections.Generic;

namespace Lily.Microservice.Microparts.ServiceCenter.Routing
{
    public interface IRoutingStrategy<T>
    {
        T RouteTo(IList<T> services);
        RouteStrategy Strategy { get; }
    }
}
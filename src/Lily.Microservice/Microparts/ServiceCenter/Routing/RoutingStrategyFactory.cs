using Lily.Microservice.AppInfo.AppInfoModel;

namespace Lily.Microservice.Microparts.ServiceCenter.Routing
{
    public class RoutingStrategyFactory
    {
        public static IRoutingStrategy<T> GetRouting<T>(RouteStrategy strategy)
            where T: ServiceInfo
        {
            IRoutingStrategy<T> routeStrategy;
            switch (strategy)
            {
                case RouteStrategy.Random:
                    {
                        routeStrategy = new RandomRoutingStrategy<T>();                      
                        break;
                    }
                case RouteStrategy.LocalDevelop:
                    {
                        routeStrategy = new LocalDevelopRoutingStrategy<T>();
                        break;
                    }
                default:
                    routeStrategy = new RoundRobinRoutingStrategy<T>();
                    break;
            }
            return routeStrategy;
        }
    }
}

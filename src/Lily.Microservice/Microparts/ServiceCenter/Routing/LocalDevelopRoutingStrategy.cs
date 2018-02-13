using System.Collections.Generic;
using System.Linq;
using Lily.Microservice.AppInfo.AppInfoModel;
using Lily.Microservice.Common.Helper;

namespace Lily.Microservice.Microparts.ServiceCenter.Routing
{
    public class LocalDevelopRoutingStrategy<T> : IRoutingStrategy<T>
                where T : ServiceInfo
    {
        public RouteStrategy Strategy { get; } = RouteStrategy.LocalDevelop;
        readonly string hostIp = IPUtil.GetHostIP();

        public T RouteTo(IList<T> services)
        {
            return services?.FirstOrDefault(s => string.Compare(s.Address, hostIp, true)==0);
        }
    }
}

using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.AppConfigurtion.Imp.Attributes;
using Lily.Microservice.Microparts.ServiceCenter.Routing;

namespace Lily.Microservice.Consul.Config
{
    [ServiceCanOverrideBundle()]
    public class AppConfigOfConsul:IAppConfig
    {
        [Key("Consul/Route", IsAllPath = true)]
        public RouteStrategy Route { get; set; }
    }
}

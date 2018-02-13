using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.AppConfigurtion.Imp.Attributes;
using Lily.Microservice.AppInfo;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.Config
{
    [ServiceCanOverrideBundle()]
    public class AppConfigOfZipkin: IAppConfig
    {
        [Key("Zipkin/Address", IsAllPath = true)]
        public string Address { get; set; }
    }
}

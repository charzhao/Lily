using Lily.Microservice.AppConfigurtion;
using Lily.Microservice.AppConfigurtion.Imp.Attributes;

namespace Lily.Microservice.Microparts.HealthCheck.Config
{
    [ServiceCanOverrideBundle()]
    public class AppConfigOfHealthCheck: IAppConfig
    {
        [Key("Healthcheck/http/IntervalOfSecond", IsAllPath = true)]
        public int IntervalOfSecond { get; set; }
        [Key("Healthcheck/http/TimeoutOfSecond", IsAllPath = true)]
        public int TimeoutOfSecond { get; set; }

        [Key("Healthcheck/http/DeregisterCriticalServiceAfterOfSecond", IsAllPath = true)]
        public int DeregisterCriticalServiceAfterOfSecond { get; set; }
    }
}

namespace Lily.Microservice.Microparts.HealthCheck
{
    public class BaseMicropartOfHealthCheck: IMicropart
    {
        public MicropartType MicropartType => MicropartType.HealthCheck;
    }
}

namespace Lily.Microservice.Microparts.HealthCheck.Features.HttpHealthCheck
{
    public class HttpHealthCheck: IHealthCheck
    {
        public string HealthCheckUrl => HealthCheckHelper.DefaultHeartbeatApi;
    }
}

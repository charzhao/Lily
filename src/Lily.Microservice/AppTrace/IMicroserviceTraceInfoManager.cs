using Lily.Microservice.AppTrace.Features;

namespace Lily.Microservice.AppTrace
{
    public interface IMicroserviceTraceInfoManager
    {
        TraceInfo GetCurrentTraceInfo();
    }
}

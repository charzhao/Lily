namespace Lily.Microservice.Microparts.Log.Nlog.Imp.TraceInfo
{
    public static class TraceInfoExtension
    {
        public static string LogTraceInfo(this AppTrace.Features.TraceInfo traceInfo)
        {
            return traceInfo.TraceId;
        }
    }
}

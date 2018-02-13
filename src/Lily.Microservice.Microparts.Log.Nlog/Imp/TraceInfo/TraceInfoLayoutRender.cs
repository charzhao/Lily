using System;
using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace Lily.Microservice.Microparts.Log.Nlog.Imp.TraceInfo
{
    [LayoutRenderer("traceInfo")]
    public class TraceInfoLayoutRender : LayoutRenderer
    {
        public static Func<AppTrace.Features.TraceInfo> GetTraceInfo { get; set; } =()=> AppTrace.Features.TraceInfo.Empty;
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(GetTraceInfo().LogTraceInfo());
        }
    }
}

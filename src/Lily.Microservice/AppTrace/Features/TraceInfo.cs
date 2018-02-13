using System;

namespace Lily.Microservice.AppTrace.Features
{
    public class TraceInfo
    {
        public TraceInfo(string traceid,string traceName="Empty")
        {
            TraceId = traceid;
        }
        private static readonly TraceInfo EmptyTraceInfo=new TraceInfo(Guid.Empty.ToString());
        public static TraceInfo Empty => EmptyTraceInfo;

        public string TraceId { get; }

        public string TraceName { get; set; }
    }
}
using Lily.Microservice.AppTrace.Features;
using Lily.Microservice.Microparts.CallChain;

namespace Lily.Microservice.AppTrace.Imp
{
    internal class MicroserviceTraceInfoManager: IMicroserviceTraceInfoManager
    {
        private readonly IMicropartOfCallTree _micropartOfCallTree;
        public MicroserviceTraceInfoManager(IMicropartOfCallTree micropartOfCallTree)
        {
            _micropartOfCallTree = micropartOfCallTree;
        }
        public TraceInfo GetCurrentTraceInfo()
        {
            var traceInfoOfCallTree= _micropartOfCallTree.GetTraceInfo();
            return new TraceInfo(traceInfoOfCallTree.TraceId);
        }
    }
}

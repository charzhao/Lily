namespace Lily.Microservice.Microparts.CallChain.Features
{
    public class TraceInfoOfCallTree
    {
        public TraceInfoOfCallTree(string traceId)
        {
            TraceId = traceId;
        }
        public string TraceId { get; private set; }
    }
}

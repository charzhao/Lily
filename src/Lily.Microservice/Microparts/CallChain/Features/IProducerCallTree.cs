using System.Runtime.CompilerServices;

namespace Lily.Microservice.Microparts.CallChain.Features
{
    public interface IProducerCallTree: ICallTreeFinished
    {
        void TraceStart(
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "");
    }
}

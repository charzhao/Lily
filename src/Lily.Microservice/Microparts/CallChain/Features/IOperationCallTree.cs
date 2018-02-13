using System.Runtime.CompilerServices;

namespace Lily.Microservice.Microparts.CallChain.Features
{
    public interface IOperationCallTree: ICallTreeFinished
    {
        void TraceStart(
            string operationName,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "");

        void TraceStartWithNew(
            string operationName,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "");
    }
}
using System.Runtime.CompilerServices;
using Lily.Microservice.Microparts.CallChain.Features;

namespace Lily.Microservice.Microparts.CallChain
{
    public  interface IMicropartOfCallTree:IMicropart
    {
        IOperationCallTree GetOperationCallTree(
            string operationName,
            bool isNewTrace=false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "");

        IConsumerCallTree GetConsumerCallTree(
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "");

        IProducerCallTree GetProducerCallTree(
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "");

        TracingHttpDelegatingHandle GetHttpDelegatingHandle();

        void OpenNewTrace();

        TraceInfoOfCallTree GetTraceInfo();
    }
}

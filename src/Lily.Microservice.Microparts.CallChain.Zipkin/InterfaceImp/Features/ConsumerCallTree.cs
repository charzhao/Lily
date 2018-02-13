using System.IO;
using System.Runtime.CompilerServices;
using Lily.Microservice.AppInfo;
using Lily.Microservice.Microparts.CallChain.Features;
using Lily.Microservice.Microparts.CallChain.Zipkin.Imp.Traces;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp.Features
{
    internal class ConsumerCallTree : BaseCallTree, IConsumerCallTree
    {
        public ConsumerCallTree()
        {
        }
        public ConsumerCallTree(
            string operationMemberName,
            string operationFilePath)
        {
            TraceStartInternal(operationMemberName, operationFilePath);
        }

        private void TraceStartInternal(string operationMemberName, string operationFilePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(operationFilePath);
            CallTreeTrace = new ConsumerTrace(
                AppInfoProvider.Service.Name,
                $"{fileName}:{operationMemberName}");
        }
        public void TraceStart(
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            var fileName = Path.GetFileNameWithoutExtension(callerFilePath);
            CallTreeTrace = new ConsumerTrace(
                AppInfoProvider.Service.Name,
                $"{fileName}:{callerMemberName}");
        }
    }
}

using System.IO;
using System.Runtime.CompilerServices;
using Lily.Microservice.AppInfo;
using Lily.Microservice.Microparts.CallChain.Features;
using Lily.Microservice.Microparts.CallChain.Zipkin.Imp.Traces;
using zipkin4net;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp.Features
{
    internal class OperationCallTree: BaseCallTree,IOperationCallTree
    {

        public OperationCallTree()
        {
        }

        public OperationCallTree(
            string operationName,
            string operationMemberName,
            string operationFilePath, bool isNewTrace = false)
        {
            if (isNewTrace)
            {
                TraceStartWithNewInternal(operationName,operationMemberName, operationFilePath);
            }
            else
            {
                TraceStartInternal(operationName,operationMemberName, operationFilePath);
            }

        }

        private void TraceStartInternal(string operationName,string operationMemberName, string operationFilePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(operationFilePath);

            CallTreeTrace = new LocalOperationTrace(
                AppInfoProvider.Service.Name,
                $"{fileName}:{operationMemberName}",
                operationName);
        }
        public  void TraceStart(
            string operationName,
            [CallerMemberName]string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            TraceStartInternal(operationName,callerMemberName, callerFilePath);
        }

        private void TraceStartWithNewInternal(
            string operationName,
           string operationMemberName,
            string operationFilePath )
        {
            var trace = Trace.Create();

            Trace.Current = trace;

            var fileName = Path.GetFileNameWithoutExtension(operationFilePath);
            using (var serverTrace = new ServerTrace(AppInfoProvider.Service.Name, $"{fileName}/{operationMemberName}"))
            {
                //empty
            }

            TraceStartInternal(operationName,operationMemberName, operationFilePath);
        }

        public void TraceStartWithNew(
            string operationName,
            [CallerMemberName]string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            TraceStartWithNewInternal(operationName,callerMemberName, callerFilePath);
        }
    }
}
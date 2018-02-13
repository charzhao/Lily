using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Lily.Microservice.AppInfo;
using Lily.Microservice.Exceptions;
using Lily.Microservice.Microparts.CallChain.Features;
using Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp.Features;
using Microsoft.Extensions.Logging;
using zipkin4net;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp
{
    internal class MicroPartOfZipkin : BaseMicropartOfCallTree, IMicropartOfCallTree
    {
        private readonly ILoggerFactory _loggerFactory;

        public MicroPartOfZipkin(
            ILoggerFactory loggerFactory,
            IEnumerable<IMicroPartInfo> microServiceParts,
            IMicropartStatusManager micropartStatusManager
            )
        {
            _loggerFactory = loggerFactory;

            var micropartInfoOfZipkin = micropartStatusManager.UpdateStatus<MicropartInfoOfZipkin>(MicropartType);
            if (!micropartInfoOfZipkin.IsEnabled)
            {
                throw new MicroServicePartNotEnabledException(micropartInfoOfZipkin.MicroServicePartName);
            }
        }
        public IOperationCallTree GetOperationCallTree(
            string operationName, bool isNewTrace = false,
            [CallerMemberName]string callerMemberName = "",
            [CallerFilePath]string callerFilePath = "")
        {
            return new OperationCallTree(
                operationName, callerMemberName, callerFilePath, isNewTrace);
        }

        public IConsumerCallTree GetConsumerCallTree(
            [CallerMemberName]string callerMemberName = "",
            [CallerFilePath]string callerFilePath = "")
        {
            return new ConsumerCallTree(callerMemberName, callerFilePath);
        }

        public IProducerCallTree GetProducerCallTree(
            [CallerMemberName]string callerMemberName = "",
            [CallerFilePath]string callerFilePath = "")
        {
            return new ProducerCallTree(callerMemberName, callerFilePath);
        }

        public TracingHttpDelegatingHandle GetHttpDelegatingHandle()
        {
            return new TracingHttpDelegatingHandleOfZipkin(AppInfoProvider.Service.Name);
        }

        public void OpenNewTrace()
        {
            var trace = Trace.Create();
            Trace.Current = trace;
        }

        public TraceInfoOfCallTree GetTraceInfo()
        {
            if (Trace.Current == null)
            {
                return new TraceInfoOfCallTree(Guid.Empty.ToString());
            }
            return new TraceInfoOfCallTree(Trace.Current.CorrelationId.ToString());
        }
    }
}

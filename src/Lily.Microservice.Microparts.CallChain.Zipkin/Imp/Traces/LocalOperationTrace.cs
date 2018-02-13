using System;
using zipkin4net;
using zipkin4net.Annotation;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.Imp.Traces
{
    internal class LocalOperationTrace:IDisposable
    {
        public zipkin4net.Trace Trace { get; }

        public LocalOperationTrace(string serviceName, string rpc, string operactionName)
        {
            if (zipkin4net.Trace.Current != null)
            {
                Trace = zipkin4net.Trace.Current.Child();
            }

            Trace.Record(Annotations.Rpc(rpc));
            Trace.Record(Annotations.LocalOperationStart(operactionName));
            Trace.Record(Annotations.ServiceName(serviceName));
            
        }

        public void AddAnnotation(IAnnotation annotation)
        {
            Trace.Record(annotation);
        }

        public void Error(Exception ex)
        {
            Trace.Record(Annotations.Tag("error", ex.Message));
        }

        public void Dispose()
        {
            Trace.Record(Annotations.LocalOperationStop());
        }
    }
}

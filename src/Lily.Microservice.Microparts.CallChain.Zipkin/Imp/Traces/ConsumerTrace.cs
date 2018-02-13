using System;
using Lily.Microservice.Common.Helper;
using zipkin4net;
using zipkin4net.Annotation;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.Imp.Traces
{
    internal class ConsumerTrace : IDisposable
    {
        public zipkin4net.Trace Trace { get; }

        public ConsumerTrace(string serviceName, string rpc)
        {
            if (zipkin4net.Trace.Current != null)
            {
                Trace = zipkin4net.Trace.Current.Child();
            }

            Trace.Record(Annotations.Rpc(rpc));
            Trace.Record(Annotations.MessageAddr(serviceName, UtilHelper.CreateIPEndPoint("127.1.1.1:8080")));
            Trace.Record(Annotations.ConsumerStart());
            Trace.Record(Annotations.ServiceName(serviceName));
            
        }

        public void AddAnnotation(IAnnotation annotation)
        {
            Trace.Record(annotation);
        }

        public virtual void Error(Exception ex)
        {
            Trace.Record(Annotations.Tag("error", ex.Message));
        }

        public void Dispose()
        {
            Trace.Record(Annotations.ConsumerStop());
        }
    }
}

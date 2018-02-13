using System;
using Lily.Microservice.Microparts.CallChain.Features;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.InterfaceImp.Features
{
    internal class BaseCallTree: ICallTreeFinished
    {
        protected IDisposable CallTreeTrace { get; set; }
        public void Dispose()
        {
            CallTreeTrace.Dispose();
        }

        public void TraceFinished()
        {
            CallTreeTrace.Dispose();
        }
    }
}

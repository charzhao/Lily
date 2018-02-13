using System;

namespace Lily.Microservice.Microparts.CallChain.Features
{
    public interface ICallTreeFinished:IDisposable
    {
        void TraceFinished();
    }
}

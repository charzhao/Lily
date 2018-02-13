using System;
using Lily.Microservice.Microparts;

namespace Lily.Microservice
{
    public interface IMicroserviceBuilder
    {
        IServiceProvider ApplicationServices { get; set; }
        IMicroserviceBuilder UseMicroservice(Action<IMicropartBuilder> micropartAppBuilderAction);
    }
}

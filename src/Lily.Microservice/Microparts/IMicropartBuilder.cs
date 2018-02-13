using System;

namespace Lily.Microservice.Microparts
{
    public interface IMicropartBuilder
    {
        IServiceProvider ApplicationServices { get; set; }
    }
}

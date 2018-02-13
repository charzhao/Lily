using System;

namespace Lily.Microservice.Microparts.Imp
{
    internal class MicropartBuilder:IMicropartBuilder
    {
        public MicropartBuilder(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
        }
        public IServiceProvider ApplicationServices { get; set; }
    }
}

using System;

namespace Lily.Microservice.Microparts.ServiceCenter.Consul
{
    public static class MicropartBuilderExtensions
    {
        public static IMicropartBuilder UseServiceCenterOfConsul(this IMicropartBuilder micropartBuilder)
        {
            return micropartBuilder;
        }

        public static IMicropartBuilder UseServiceCenterOfConsul(
            this IMicropartBuilder micropartBuilder,
            Action<MicropartOfServiceCenterAConsulBuilder> innerBuilderAction
            )
        {
            innerBuilderAction(new MicropartOfServiceCenterAConsulBuilder(micropartBuilder));
            return micropartBuilder;
        }
    }
}

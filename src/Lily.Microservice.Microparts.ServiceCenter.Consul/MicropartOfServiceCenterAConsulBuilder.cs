using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.ServiceCenter.Consul
{
    public class MicropartOfServiceCenterAConsulBuilder
    {
        private IMicropartBuilder _micropartBuilder;
        public MicropartOfServiceCenterAConsulBuilder(IMicropartBuilder micropartBuilder)
        {
            _micropartBuilder = micropartBuilder;
        }

        public void RegisterMyselfToServiceCenter()
        {
            _micropartBuilder.ApplicationServices.GetService<IMicropartOfServiceCenter>().RegisterService();
        }
    }
}

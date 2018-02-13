using Lily.Microservice.AppInfo.AppInfoModel;

namespace Lily.Microservice.Microparts.ServiceCenter
{
    public interface IMicropartOfServiceCenter: IMicropart
    {
        void RegisterService();

        ServiceInfo DiscoveryService(string sKey);
    }
}

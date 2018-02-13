using System;
using Lily.Microservice.AppInfo.AppInfoModel;
using Lily.Microservice.Exceptions;
using Lily.Microservice.Microparts.ServiceCenter.Consul.Imp;
using Microsoft.Extensions.Logging;

namespace Lily.Microservice.Microparts.ServiceCenter.Consul.InterfaceImp
{
    internal class MicropartOfServiceCenter :BaseMicropartOfServiceCenter, IMicropartOfServiceCenter
    {
        private readonly ServiceCenterService _serviceCenterService;

        public MicropartOfServiceCenter(
            ILoggerFactory loggerFactory,
            IMicropartStatusManager micropartStatusManager)
        {
            _serviceCenterService = new ServiceCenterService(loggerFactory?.CreateLogger("Consul"));
            var micropartInfoOfServiceCenter = micropartStatusManager.UpdateStatus<MicropartInfoOfServiceCenter>(MicropartType);
            if (!micropartInfoOfServiceCenter.IsEnabled)
            {
                throw new MicroServicePartNotEnabledException(micropartInfoOfServiceCenter.MicroServicePartName);
            }
        }

        public void RegisterService()
        {
            _serviceCenterService.ServiceRegister();
        }

        public ServiceInfo DiscoveryService(string sKey)
        {
            if (string.IsNullOrWhiteSpace(sKey))
                throw new ArgumentNullException("sKey");
            return _serviceCenterService.GetServiceAsync(sKey).GetAwaiter().GetResult();
        }
    }
}

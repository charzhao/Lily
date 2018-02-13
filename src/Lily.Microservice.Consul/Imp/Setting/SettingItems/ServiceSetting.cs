using System;
using Consul;
using Lily.Microservice.AppInfo;
using Lily.Microservice.AppInfo.AppInfoModel;
using Lily.Microservice.Common.Helper;
using Lily.Microservice.Microparts.HealthCheck.Features;

namespace Lily.Microservice.Consul.Imp.Setting.SettingItems
{
    public class ServiceSetting
    {
        const int DefaultTime = 10;
        /// <summary>
        /// reserve a place to authorization
        /// </summary>
        public string Key { get; set; }
        public bool EnableTagOverride { get; set; }
        //check url must be ping success by consul server
        public HealthCheckSetting Check { get; set; }

        public AgentServiceRegistration Convert()
        {
            var service = new AgentServiceRegistration
            {
                ID = AppInfoProvider.Service.Id ??$"{AppInfoProvider.Service.Name}:{IPUtil.GetHostName()}",
                Name = AppInfoProvider.Service.Name,
                Tags = AppInfoProvider.Service.Tags,
                Port = AppInfoProvider.Service.Port,
                Address = AppInfoProvider.Service.Address,
                EnableTagOverride = EnableTagOverride
                       
            };
            if (Check == null)
            {
                Check = new HealthCheckSetting
                {
                    HTTP = HealthCheckHelper.GetHttpHealthUrl()
                };
            }
            service.Check = new AgentServiceCheck {
                TCP= Check.TCP,
                Timeout = TimeSpan.FromSeconds(Check.Timeout ?? DefaultTime),
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(Check.DeregisterCriticalServiceAfter ?? DefaultTime),
                Interval =TimeSpan.FromSeconds(Check.Interval?? DefaultTime),
                TLSSkipVerify=Check.TLSSkipVerify,
                HTTP = Check.HTTP,
            };
            return service;
        }
    }
}

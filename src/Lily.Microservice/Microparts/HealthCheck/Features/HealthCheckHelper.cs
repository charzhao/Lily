using System;
using System.Collections.Generic;
using System.Text;
using Lily.Microservice.AppInfo;

namespace Lily.Microservice.Microparts.HealthCheck.Features
{
    public static class HealthCheckHelper
    {
        public static readonly string DefaultHeartbeatApi = "/api/F5";
        public static string GetHttpHealthUrl()
        {
            var url = AppInfoProvider.Service.GetBaseHttpUrlWithAppend(DefaultHeartbeatApi).ToString();
            return url;
        }
    }
}

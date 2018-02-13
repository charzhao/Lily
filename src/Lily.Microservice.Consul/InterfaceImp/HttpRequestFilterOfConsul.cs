using System;
using Lily.Microservice.AspNetCore.Imp;
using Lily.Microservice.Microparts.HealthCheck.Features;
using Microsoft.AspNetCore.Http;

namespace Lily.Microservice.Consul.InterfaceImp
{
    public class HttpRequestFilterOfConsul: HttpRequestFilter
    {
        public override bool ShouldBeFiltered(HttpRequest request)
        {
            return string.Compare(
                       request.Path.ToString(),
                       HealthCheckHelper.DefaultHeartbeatApi,
                       StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}

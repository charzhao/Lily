using System;
using Lily.Microservice.AspNetCore.Imp;
using Lily.Microservice.Microparts.HealthCheck.Features.HttpHealthCheck;
using Microsoft.AspNetCore.Http;

namespace Lily.Microservice.Microparts.HealthCheck.AspNetCore.Imp
{
    public class HttpRequestFilterOfHealthCheck: HttpRequestFilter
    {
        public override bool ShouldBeFiltered(HttpRequest request)
        {
            var httpHealtheCheck = new HttpHealthCheck();
            return string.Compare(
                       request.Path.ToString(),
                       httpHealtheCheck.HealthCheckUrl,
                       StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}

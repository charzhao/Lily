using Lily.Microservice.AppTrace.Features;
using Microsoft.AspNetCore.Http;

namespace Lily.Microservice.AspNetCore.Imp
{
    public abstract class HttpRequestFilter: IMicroserviceFilter<HttpRequest>
    {
        public abstract bool ShouldBeFiltered(HttpRequest request);

    }
}

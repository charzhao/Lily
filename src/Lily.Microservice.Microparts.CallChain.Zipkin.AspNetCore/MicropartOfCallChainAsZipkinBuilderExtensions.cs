using Lily.Microservice.AppInfo;
using Lily.Microservice.AppTrace;
using Lily.Microservice.AspNetCore.Imp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using zipkin4net;
using zipkin4net.Transport;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.AspNetCore
{
    public static class MicropartOfCallChainAsZipkinBuilderExtensions
    {
        public static void UseMyTracingMiddlewareOfAspNetCore(
            this MicropartOfCallChainAsZipkinBuilder micropartOfCallChainAsZipkinBuilder, 
            IApplicationBuilder app)
        {
            var extractor = new ZipkinHttpTraceExtractor();
            app.Use(async (context, next) =>
            {
                var request = context.Request;
                var httpRequestFilter = app.ApplicationServices.GetService<IMicroserviceFilterManager<HttpRequestFilter, HttpRequest>>();
                if (httpRequestFilter.ShouldBeFiltered(request))
                {
                    return;
                }

                if (!extractor.TryExtract(request.Headers, (c, key) => c[key], out var trace))
                {
                    trace = Trace.Create();
                }
                Trace.Current = trace;
                using (var serverTrace = new ServerTrace(AppInfoProvider.Service.Name, $"{request.Path}/{request.Method}"))
                {
                    trace.Record(Annotations.Tag("http.host", request.Host.ToString()));
                    trace.Record(Annotations.Tag("http.uri", UriHelper.GetDisplayUrl(request)));
                    trace.Record(Annotations.Tag("http.path", request.Path));
                    await serverTrace.TracedActionAsync(next());
                }
            });
        }
    }
}

using Lily.Microservice;
using Lily.Microservice.AspNetCore;
using Lily.Microservice.Consul;
using Lily.Microservice.Microparts.CallChain.Zipkin;
using Lily.Microservice.Microparts.CallChain.Zipkin.AspNetCore;
using Lily.Microservice.Microparts.ConfigurationCenter.Consul;
using Lily.Microservice.Microparts.EventBus.RabbitMq;
using Lily.Microservice.Microparts.HealthCheck.AspNetCore;
using Lily.Microservice.Microparts.Log.Nlog;
using Lily.Microservice.Microparts.ServiceCenter.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Samples.Lily.Microservice.WebApiFacade
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMicroservie(micropartServiceCollection =>
            {
                micropartServiceCollection
                    .AddCallTreeOfZipkin()
                    .AddConfigurationCenterOfConsul()
                    .AddServiceCenterOfConsul()
                    .AddRabbitMqAsEventBus()
                    .AddLogOfNLog();
            })
            .AddMicroservieOfAspNetCore();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var microserviceBuilder = app.ApplicationServices.GetService<IMicroserviceBuilder>()
                .UseConsul()
                .UseMicroservice(micropartBuilder =>
                {
                    micropartBuilder
                          //.UseRabbitMqAsEventBus()
                        .UseHttpHealthCheckOfAspNetCore(app)
                        .UseServiceCenterOfConsul(innerBuilder =>
                        {
                            innerBuilder.RegisterMyselfToServiceCenter();
                        })
                        .UseCallTraceOfZipkin(innerBuilder =>
                        {
                            innerBuilder.UseMyTracingMiddlewareOfAspNetCore(app);
                        })
                        .UseLogOfNLog();
                }).UseMicroservieOfAspNetCore(app);

            app.UseMvc();
        }
    }
}

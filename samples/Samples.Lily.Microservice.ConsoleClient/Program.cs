using System;
using Lily.Microservice;
using Lily.Microservice.AppLifetime;
using Lily.Microservice.Consul;
using Lily.Microservice.Microparts.CallChain.Zipkin;
using Lily.Microservice.Microparts.ConfigurationCenter.Consul;
using Lily.Microservice.Microparts.EventBus.RabbitMq;
using Lily.Microservice.Microparts.Log.Nlog;
using Lily.Microservice.Microparts.ServiceCenter.Consul;
using Microsoft.Extensions.DependencyInjection;

namespace Samples.Lily.Microservice.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MicroserviceBuilder.Init();
            var serviceProvider = new ServiceCollection()
                .AddMicroservie(micropartServiceCollection =>
                {
                    micropartServiceCollection
                        .AddCallTreeOfZipkin()
                        .AddConfigurationCenterOfConsul()
                        .AddServiceCenterOfConsul()
                        .AddRabbitMqAsEventBus()
                        .AddLogOfNLog();
                })
                .AddTransient<Startup>()
                .BuildServiceProvider();
            ;


            var microserviceBuilder = serviceProvider.GetService<IMicroserviceBuilder>()
                .UseConsul()
                .UseMicroservice(micropartBuilder =>
                {
                    micropartBuilder
                        //.UseRabbitMqAsEventBus()
                        .UseServiceCenterOfConsul()
                        .UseCallTraceOfZipkin()
                        .UseLogOfNLog();
                });
            var appLifetime=serviceProvider.GetService<IAppLifetime>();
            appLifetime.StartApplication();

            serviceProvider.GetService<Startup>().Run();
            Console.WriteLine("Hello World!");
        }


    }
}

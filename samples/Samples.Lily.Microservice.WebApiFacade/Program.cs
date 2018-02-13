using Lily.Microservice;
using Lily.Microservice.AppInfo;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Samples.Lily.Microservice.WebApiFacade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MicroserviceBuilder.Init();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://*:{AppInfoProvider.Service.Port}")
                .Build();
    }
}

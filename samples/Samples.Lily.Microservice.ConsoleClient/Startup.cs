using System;
using Lily.Microservice.Microparts.CallChain;
using Lily.Microservice.Microparts.ServiceCenter;

namespace Samples.Lily.Microservice.ConsoleClient
{
   
    public class Startup
    {
        private readonly IMicropartOfCallTree _micropartOfCallTree;
        private readonly IMicropartOfServiceCenter _micropartOfServiceCenter;
        public Startup(
            IMicropartOfCallTree micropartOfCallTree,
            IMicropartOfServiceCenter micropartOfServiceCenter)
        {
            _micropartOfCallTree = micropartOfCallTree;
            _micropartOfServiceCenter = micropartOfServiceCenter;

        }

        public void Run()
        {
            Console.WriteLine("*******************************************" + "\n" +
                              "quit: to exit," + "\n" +
                              "calltree :for calltree demo" + "\n" +
                              "press any other key continue");
            string value = Console.ReadLine();
            if (value == "quit")
            {
                return;
            }

            if (String.Compare(value, "calltree", StringComparison.OrdinalIgnoreCase) == 0)
            {
                CallTreeTest();
            }


            Run();
        }

        private void CallTreeTest()
        {
            using (_micropartOfCallTree.GetOperationCallTree("ServiceCenterTest", true))
            {
                var serviceInfo = _micropartOfServiceCenter.DiscoveryService("WebApiFacade");
                var results = ApiCall
                    .Call(serviceInfo.GetBaseHttpUrl(), "api/Values", _micropartOfCallTree)
                    .GetAwaiter()
                    .GetResult();
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}

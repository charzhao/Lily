using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Lily.Microservice.Microparts.CallChain;

namespace Samples.Lily.Microservice.ConsoleClient
{
    public static class ApiCall
    {
        public static async Task<string[]> Call(Uri serviceUrl, string apiPath, IMicropartOfCallTree callTreeFactory)
        {
            using (var httpClient = new HttpClient(callTreeFactory.GetHttpDelegatingHandle()))
            {
                httpClient.BaseAddress = serviceUrl;
                var response = await httpClient.GetAsync(apiPath);
                if (!response.IsSuccessStatusCode)
                {
                    //await HttpContext.Response.WriteAsync(response.ReasonPhrase);
                    return new[] { response.ReasonPhrase };
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //await HttpContext.Response.WriteAsync(content);
                    return new string[] { content };
                }
            }
        }

        public static async Task<string[]> CallWithOutCallTree(string serviceUrl, string apiPath, [CallerMemberName]string callerName = "")
        {
            using (var httpClient = new HttpClient())
            {

                httpClient.BaseAddress = new Uri(serviceUrl);
                var response = await httpClient.GetAsync(apiPath);
                if (!response.IsSuccessStatusCode)
                {
                    //await HttpContext.Response.WriteAsync(response.ReasonPhrase);
                    return new[] { response.ReasonPhrase };
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //await HttpContext.Response.WriteAsync(content);
                    return new string[] { content };
                }
            }
        }
    }
}

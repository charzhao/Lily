using System.Net;
using System.Net.Sockets;

namespace Lily.Microservice.Common.Helper
{
    public static class IPUtil
    {
        public static string GetHostIP()
        {
            var addresses = Dns.GetHostAddresses(string.Empty);
            foreach(var address in addresses)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                    return address.ToString();
            }
            return string.Empty;
        }

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }
    }
}

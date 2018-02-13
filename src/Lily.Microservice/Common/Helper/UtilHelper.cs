using System;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Lily.Microservice.Common.Helper
{
    public static class UtilHelper
    {
        public static string GetCurrentMethodName([CallerMemberName]string callerName = "")
        {
            return callerName;
        }

        // Handles IPv4 and IPv6 notation.
        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length < 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (ep.Length > 2)
            {
                if (!IPAddress.TryParse(string.Join(":", ep, 0, ep.Length - 1), out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            else
            {
                if (!IPAddress.TryParse(ep[0], out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            int port;
            if (!int.TryParse(ep[ep.Length - 1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        private static int _seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> s_random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        public static int Next(int lowerBound, int upperBound) => s_random.Value.Next(lowerBound, upperBound);
    }
}

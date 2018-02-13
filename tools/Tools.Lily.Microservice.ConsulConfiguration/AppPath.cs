using System.IO;
using System.Reflection;

namespace Tools.Lily.Microservice.ConsulConfiguration
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AppPath
    {

        public static string GetBinPath()
        {
            var binPath= Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return binPath;
        }
    }
}

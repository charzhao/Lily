using System.IO;
using System.Reflection;

namespace Lily.Microservice.Common.Helper
{
    public class AppPath
    {

        public static string GetBinPath()
        {
            var binPath= Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return binPath;
        }

        public static string GetAppConfigurationPath()
        {
            var binPath = GetBinPath();
            var appConfigurationPath = Path.Combine(binPath, "appConfiguration");
            if (!Directory.Exists(appConfigurationPath))
            {
                Directory.CreateDirectory(appConfigurationPath);
            }
            return appConfigurationPath;
        }
    }
}

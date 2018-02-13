using System;
using System.IO;
using System.Linq;

namespace Lily.Microservice.Tools.ConsulConfiguration
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var binPath = AppPath.GetBinPath();
            var configsPath=$"{binPath}/Configs";
            var files = Directory.GetFiles(configsPath);
            foreach (var file in files)
            {
                string extension = Path.GetExtension(file);
                string[] str = new string[] {".json" };
                if (!str.Contains(extension))
                {
                    continue;
                }

                try
                {
                    ConfigFileConvertor.ConvertTo(file);
                    Console.WriteLine($"{file} converted in success");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"{file} converted in failure");
                }
               
            }

            Console.ReadLine();

        }
    }
}

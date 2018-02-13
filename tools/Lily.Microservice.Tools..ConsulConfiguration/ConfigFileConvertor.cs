using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Lily.Microservice.Tools.ConsulConfiguration
{
    public static class ConfigFileConvertor
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        const string FileExt = ".json";
        public static string ConvertTo(string fileName)
        {                     
            try
            {
                IList<ConsulKeyValueFormat.KeyValuePair> kvs = GetFromFile(fileName);

                var rootFolder = Directory.GetParent(fileName).Parent;
                var folder = Directory.GetParent(fileName).FullName;
                if (rootFolder != null)
                {
                    folder = rootFolder.FullName;
                }
                folder = $"{folder}/CovertedResult";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var fileNameOfConverted = Path.Combine(folder, Path.GetFileName(fileName));
                return SetToFile(kvs, fileNameOfConverted);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private static IList<ConsulKeyValueFormat.KeyValuePair> GetFromFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        return Serializer.Deserialize<List<ConsulKeyValueFormat.KeyValuePair>>(jsonReader);
                    }
                }
            }
        }

        private static string SetToFile(IList<ConsulKeyValueFormat.KeyValuePair> kvs, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(kvs, Formatting.Indented));
                //开始写入
                fs.Write(data, 0, data.Length);
            }
            return filePath;
        }
    }
}

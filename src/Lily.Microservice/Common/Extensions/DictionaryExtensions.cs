using System.Collections.Generic;

namespace Lily.Microservice.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate<Tkey, Tvalue>(this IDictionary<Tkey, Tvalue> dict, Tkey key, Tvalue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lily.Microservice.Microparts.ConfigurationCenter.Model;

namespace Lily.Microservice.Microparts.ConfigurationCenter
{
    public interface IMicropartOfConfigurationCenter: IMicropart
    {
        /// <summary>
        /// write a new value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, string value);

        /// <summary>
        /// lookup value with key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// if not found, will return null
        /// </returns>
        Task<KeyValueConfig> GetAsync(string key);

        /// <summary>
        /// Delete is used to delete a single key.
        /// </summary>
        /// <param name="key">The key name to delete</param>
        /// <returns>
        /// </returns>
        Task<bool> DeleteAsync(string key);

        /// <summary>
        /// lookup value with key and watch change
        /// </summary>
        /// <param name="key"></param>
        /// <param name="watchCallback"></param>
        /// <returns>
        /// if not found, will return null
        /// </returns>
        Task<KeyValueConfig> GetAndWatchCacheAsync(string key, Action<WatcherEventArg<KeyValueConfig>> watchCallback =null);

        /// <summary> 
        /// List is used to lookup all kv under a prefix and watch change
        /// </summary>
        /// <param name="prefix">
        /// The prefix to search under. Does not have to be a full path 
        /// e.g. a prefix of "ab" will find keys "abcd" and "ab11" but not "acdc"
        /// </param>
        /// <param name="watchCallback"></param>
        /// <returns>
        /// if not found, will return null
        /// </returns>
        Task<IList<KeyValueConfig>> ListAndWatchCacheAsync(string prefix, Action<WatcherEventArg<IList<KeyValueConfig>>> watchCallback = null);

        /// <summary>
        /// used to apply multiple KV operations in a single, atomic transaction
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task SetByTransaction(IList<KeyValueConfig> settings);

        /// <summary>
        /// List is used to lookup all kv under a prefix
        /// </summary>
        /// <param name="prefix">
        /// The prefix to search under. Does not have to be a full path 
        /// e.g. a prefix of "ab" will find keys "abcd" and "ab11" but not "acdc"
        /// </param>
        /// <returns>
        /// if not found, will return null
        /// </returns>
        Task<IList<KeyValueConfig>> ListAsync(string prefix);

        /// <summary>
        /// DeleteTree is used to delete all keys under a prefix
        /// </summary>
        /// <param name="prefix">The key prefix to delete from</param>
        /// <returns></returns>
        Task<bool> DeleteTreeAsync(string prefix);


    }
}

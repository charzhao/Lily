using System;

namespace Lily.Microservice.Microparts.ConfigurationCenter.Model
{
    public class WatcherEventArg<T>:EventArgs
    {
        /// <summary>
        /// The observed object
        /// </summary>
        public string WatchKey { get; set; }

        public T WatchValue { get; set; }
    }
}

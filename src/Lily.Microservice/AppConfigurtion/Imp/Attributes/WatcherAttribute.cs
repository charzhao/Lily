using System;

namespace Lily.Microservice.AppConfigurtion.Imp.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class WatcherAttribute: Attribute
    {
        /// <summary>
        /// watchCallBack
        /// </summary>
        /// <param name="key">The observed object</param>
        /// <param name="value"></param>
        public abstract void WatchCallBack(string key, string value);
    }
}

using System;
using Lily.Microservice.AppInfo;
using Lily.Microservice.Exceptions;

namespace Lily.Microservice.AppConfigurtion.Imp.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited =false)]
    public class ServiceCanOverrideBundleAttribute: Attribute
    {
        public string ServiceName => AppInfoProvider.Service.Name;

        private string _defaultPath= AppInfoProvider.AppConfigDefaultkey;
        public string DefaultPath
        {
            get { return this._defaultPath; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _defaultPath = value.Trim('/');
            }
        }

        public string ConfigFileName
        {
            get;
            set;
        }
    }
}

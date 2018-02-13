using System;
using Lily.Microservice.Common.Helper;
using Lily.Microservice.Exceptions;

namespace Lily.Microservice.AppInfo.AppInfoModel
{
    public class UrlInfo
    {
        public string Schema { get; set; } = "http";

        public int Port { get; set; }

        private string _address = IPUtil.GetHostIP();
        public string Address
        {
            get => _address;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _address = value;
            }
        }

        public Uri GetBaseHttpUrl()
        {
            var builder = new UriBuilder(Schema, Address, Port);
            return builder.Uri;
        }

        public Uri GetBaseHttpUrlWithAppend(string appended)
        {
            var builder = new UriBuilder(Schema, Address, Port, appended);
            return builder.Uri;
        }

        public virtual void Validate()
        {
            if (Port <= 0)
            {
                throw new ValidationException(nameof(Port), "can not be null or empty");
            }

            if (string.IsNullOrWhiteSpace(Address))
            {
                throw new ValidationException(nameof(Address), "can not be null or empty");
            }
        }
    }
}

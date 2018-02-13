using Lily.Microservice.Exceptions;

namespace Lily.Microservice.AppInfo.AppInfoModel
{
    public class ServiceInfo: UrlInfo
    {
        public ServiceInfo()
        {

        }
        public ServiceInfo(string id)
        {
            Id = id;
        }
        public string Id { get; protected set; }

        public string Name { get; set; }


        public string[] Tags { get; set; }


        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ValidationException(nameof(Name), "can not be null or empty");
            }
        }
    }
}

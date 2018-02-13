using Lily.Microservice.AppInfo.AppInfoModel;

namespace Lily.Microservice.AppInfo
{
    public static class AppInfoProvider
    {
        public static readonly ServiceInfo Service = new ServiceInfo();
        public static readonly string AuthorityUrl = "https://localhost/ThinktectureIdentityService/";
        public static readonly UrlPathModel DefaultPageUrl =new UrlPathModel
        {
            Perfix = "",
            ControllerName = "MicroServiceStatus",
            ActionName = "Index"
        };

        public static readonly string AppConfigDefaultkey = "Default";
    }
}

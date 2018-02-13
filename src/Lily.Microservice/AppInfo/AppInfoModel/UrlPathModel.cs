namespace Lily.Microservice.AppInfo.AppInfoModel
{
    public class UrlPathModel
    {
        public UrlPathModel()
        {
            Perfix = string.Empty;
            ControllerName = string.Empty;
            ActionName = string.Empty;
        }
        public string Perfix { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public string GetUrl()
        {
            var template= $"/{Perfix}/{ControllerName}/{ActionName}".TrimEnd('/');
            template=template.TrimEnd('/').TrimEnd('/').TrimEnd('/');
            template = template.TrimStart('/').TrimStart('/').TrimStart('/');
            return $"/{template}";
        }

        public string GetTemplate()
        {
            var template= $"{Perfix}/{{controller}}/{{action}}";
            template = template.TrimStart('/').TrimStart('/').TrimStart('/');
            return  template;
            }
    }
}

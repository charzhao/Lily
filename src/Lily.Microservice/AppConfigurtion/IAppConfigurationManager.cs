namespace Lily.Microservice.AppConfigurtion
{
    public interface IAppConfigurationManager
    {
        T GetConfig<T>() where T: IAppConfig;
        void MonitorConfig<T>() where T : IAppConfig;
    }
}

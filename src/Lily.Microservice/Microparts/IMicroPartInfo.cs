namespace Lily.Microservice.Microparts
{
    public interface IMicroPartInfo
    {
        string MicroServicePartName { get;}
        bool IsEnabled { get; set; }
    }
    public abstract class BaseMicroPartInfo:IMicroPartInfo
    {
        protected BaseMicroPartInfo()
        {
            IsEnabled = true;
        }
        public abstract string MicroServicePartName { get; }
        public bool IsEnabled { get; set; }
    }
}

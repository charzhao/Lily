namespace Lily.Microservice.Microparts.EventBus
{
    public abstract class BaseMicropartOfEventBus : IMicropart
    {
        public MicropartType MicropartType => MicropartType.EventBus;
    }
}

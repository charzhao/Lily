namespace Lily.Microservice.Microparts.EventBus.Features
{
    public interface IBaseEventHandler
    {
        bool Match(EventData eventData);

        void Handle(EventData eventData);
    }
}

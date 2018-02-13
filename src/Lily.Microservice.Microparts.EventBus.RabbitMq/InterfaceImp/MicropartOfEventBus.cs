using System.Collections.Generic;
using Lily.Microservice.Microparts.EventBus.Features;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.InterfaceImp
{
    internal class MicropartOfEventBus: BaseMicropartOfEventBus,IMicropartOfEventBus
    {
        public void Publish(EventData eventData)
        {
            MessagePublishManager.Instance.Publish(eventData);
        }

        public void SubscribeEventHandle(List<IBaseEventHandler> evenHandlers)
        {
            RouteMessageToHandler.Instance.SubscribeEventHandle(evenHandlers);
        }
    }
}

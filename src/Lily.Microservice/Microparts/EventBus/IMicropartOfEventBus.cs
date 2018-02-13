using System.Collections.Generic;
using Lily.Microservice.Microparts.EventBus.Features;

namespace Lily.Microservice.Microparts.EventBus
{
    public interface IMicropartOfEventBus : IMicropart
    {
        void SubscribeEventHandle(List<IBaseEventHandler> evenHandlers);

        void Publish(EventData eventData);
    }
}

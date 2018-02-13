using System.Collections.Generic;
using Lily.Microservice.Microparts.EventBus.Features;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp
{
    public class RouteMessageToHandler
    {
        protected internal static RouteMessageToHandler Instance = new RouteMessageToHandler();

        private List<IBaseEventHandler> _eventDataHandles = new List<IBaseEventHandler>();
        public void SubscribeEventHandle(List<IBaseEventHandler> evenHandlers)
        {
            _eventDataHandles = evenHandlers;
        }

        public void HandleEventData(EventData eventData)
        {
            foreach (var handle in _eventDataHandles)
            {
                if (handle.Match(eventData))
                {
                    handle.Handle(eventData);
                }
            }
        }
    }
}

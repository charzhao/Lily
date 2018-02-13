using System.Collections.Generic;
using System.Linq;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Contract;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.EasyNetQ;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.MessageService;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp
{
    public class MessageSubscribeManager
    {
        protected internal static MessageSubscribeManager Instance = new MessageSubscribeManager();

        private SubscriberMessageMappings _mappings;
        private readonly List<IMessageListener> _listeners;

        private MessageSubscribeManager()
        {
            _listeners = new List<IMessageListener>();
        }

        internal void Initialize()
        {
            var service = MessageService.MessageService.Instance;
            _mappings = service.GetMessageEndpointMapping() ?? new SubscriberMessageMappings();

            var mappingGroup = _mappings.GroupBy(m => new { m.Value.ProviderName, m.Value.EndpointName }).ToList();
            mappingGroup.ForEach(g => AddListener(CreateListener(g.Key.EndpointName)));
        }
        private IMessageListener CreateListener(string name)
        {
            var listener = new EasyNetQListener();
            listener.Initialize(name);
            return listener;
        }
        private void AddListener(IMessageListener listener)
        {
            _listeners.Add(listener);
        }

        internal void StartAll()
        {
            _listeners.ForEach(l => l.Start());
        }

        internal void SubscribeAllMapping()
        {
            _listeners.ForEach(l =>
            {
                var eventCodeArray = _mappings.Where(m => l.Name == m.Value.EndpointName)
                                                       .Select(m => m.Key)
                                                       .ToArray();
                l.Subscribe(eventCodeArray);
            });
        }

        public void Dispose()
        {
            _listeners.ForEach(l => l.Stop());
            _listeners.Clear();
        }
    }
}

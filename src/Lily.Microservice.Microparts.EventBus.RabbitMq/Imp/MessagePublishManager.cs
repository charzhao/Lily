using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lily.Microservice.Microparts.EventBus.Features;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Contract;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.EasyNetQ;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.MessageService;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp
{
    internal class MessagePublishManager : IDisposable
    {
        protected internal static MessagePublishManager Instance = new MessagePublishManager();

        private MessagePublishManager()
        {
            _publishers = new List<IMessagePublisher>();
        }

        private PublisherMessageMappings _mappings;
        private readonly List<IMessagePublisher> _publishers;

        internal void Initialize()
        {
            var service = MessageService.MessageService.Instance;
            _mappings = service.GetPublisherProviderMapping() ?? new PublisherMessageMappings();
            var providers = _mappings.Values.SelectMany(m => m).
                                             GroupBy(m => new
                                             {
                                                 m.ProviderName,
                                             }).ToList();
            providers.ForEach(g => AddPublisher(CreatePublisher(g.Key.ProviderName)));
        }
        private IMessagePublisher CreatePublisher(string providerName)
        {
            var publisher = new EasyNetQPublisher();
            publisher.Initialize(providerName);
            return publisher;
        }
        private void AddPublisher(IMessagePublisher publisher)
        {
            _publishers.Add(publisher);
        }

        internal void StartAll()
        {
            _publishers.ForEach(p => p.Start());
        }

        public void Publish(EventData message)
        {
            if (!_mappings.ContainsKey(message.EventCode))
            {
                throw new ArgumentException($"Eventcode {message.EventCode} is not registered in mapping container. The event {message.GetType()} could not be published.");
            }
            var mappings = _mappings[message.EventCode];

            foreach (var m in mappings)
            {
                Task.Run(() => _publishers.Find(p => p.Name == m.ProviderName).Publish(message));
            }
        }

        public void Dispose()
        {
            _publishers.ForEach(l => l.Stop());
            _publishers.Clear();
        }
    }
}

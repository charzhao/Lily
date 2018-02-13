using System;
using EasyNetQ;
using Lily.Microservice.Microparts.EventBus.Features;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Contract;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Setting;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.EasyNetQ
{
    public class EasyNetQListener : IMessageListener, IDisposable
    {
        // Actually this is the same as MessagingConfiguration.Instance.ServiceRuleName now.
        // And used as Queue name in RabbitMQ
        public string Name { get; private set; }

        private IBus _listener;

        public void Initialize(string name)
        {
            Name = name;
        }

        public void Start()
        {
            try
            {
                Stop();
                var conStr = MessagingConfiguration.Instance.EasynetqConnectionString;
                _listener = RabbitHutch.CreateBus(conStr, serviceRegister =>
                {
                    serviceRegister.Register<ITypeNameSerializer>(
                        serviceProvider => new IspTypeNameSerializer());


                    serviceRegister.Register<IConventions>(
                        serviceProvider => new IspConventions(serviceProvider.Resolve<ITypeNameSerializer>()));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Stop()
        {
            try
            {
                if (_listener != null)
                {
                    _listener.Dispose();
                    _listener = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Subscribe(short[] eventCodeArray)
        {
            try
            {
                if (_listener != null)
                {
                    foreach (var eventCode in eventCodeArray)
                    {
                        _listener.Subscribe<EventData>(
                            Name,
                            HandleEvent,
                            x => x.WithTopic(eventCode.ToString()));

                        _listener.Subscribe<EventDataOfOld>(
                           Name,
                           HandleEvent,
                           x => x.WithTopic(eventCode.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        private void HandleEvent(object message)
        {
            try
            {
                if (message is EventData data)
                {
                    RouteMessageToHandler.Instance.HandleEventData(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

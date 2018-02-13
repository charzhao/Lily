using System;
using EasyNetQ;
using Lily.Microservice.Microparts.EventBus.Features;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Contract;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Setting;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.EasyNetQ
{
    public class EasyNetQPublisher : IMessagePublisher
    {
        // Actually this is always EASYNETQ now.
        // And used to distinguish each publishner(EasyNetQ|NSB|Redis, etc)
        // But it's no use now.
        public string Name { get; private set; }

        protected IBus Publisher { get; private set; }
        
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
                Publisher = RabbitHutch.CreateBus(conStr);
            }
            catch (Exception ex)
            {
                //Logger.Error(methodName, ex);
                throw ex;
            }
        }

        public void Stop()
        {
            try
            {
                //Logger.Info("Enter Method: " + methodName);
                if (Publisher != null)
                {
                    Publisher.Dispose();
                    Publisher = null;
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(methodName, ex);
                throw ex;
            }
        }

        public void Publish(EventData message)
        {
            try
            {
                Publisher.Publish(message, message.EventCode.ToString());
            }
            catch (Exception ex)
            {
                //Logger.Error("Error in publishing message>> Eventcode: " + message.EventCode, ex);
                throw ex;
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}

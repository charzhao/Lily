using System;
using EasyNetQ;
using Lily.Microservice.Microparts.EventBus.Features;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.EasyNetQ
{
    public class IspConventions : Conventions
    {
        private const string IspEventEntityAssemblyName = "Honeywell.ISP.Framework.Messaging.Core.Contracts.IBaseEvent:Honeywell.ISP.Framework.Messaging.Core.Contracts";
        public IspConventions(ITypeNameSerializer typeNameSerializer) : base(typeNameSerializer)
        {
            ExchangeNamingConvention = messageType =>
            {
                var attr = GetQueueAttribute(messageType);

                if (!string.IsNullOrEmpty(attr.ExchangeName))
                {
                    return attr.ExchangeName;
                }

                if (messageType == typeof(EventDataOfOld))
                {
                    return IspEventEntityAssemblyName;
                }
                else
                {
                    return typeNameSerializer.Serialize(messageType);
                }
            };
        }

        private QueueAttribute GetQueueAttribute(Type messageType)
        {
            return messageType.GetAttribute<QueueAttribute>() ?? new QueueAttribute(string.Empty);
        }
    }
}

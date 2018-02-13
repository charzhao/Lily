using System;
using System.Collections.Generic;

namespace Lily.Microservice.Microparts.EventBus.Features
{
    /// <summary>
    /// This is used for handle NGV Event
    /// </summary>
    public class EventData
    {
        public EventData()
        {
            EventId = Guid.NewGuid();
            OriginUtcTime = DateTime.UtcNow;
        }

        private bool _isInited;
        public void InitEventCode(short eventCode)
        {
            if (_isInited)
            {
                throw new NotSupportedException("can not change the eventCode second time");
            }
            _isInited = true;
            EventCode = eventCode;
        }

        public Guid EventId { get; }

        public short EventCode { get; set; }

        public DateTime OriginUtcTime { get; }

        public virtual IEnumerable<string> Validation()
        {
            return new List<string>();
        }
    }
}

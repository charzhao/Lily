using System;

namespace Lily.Microservice.Microparts.EventBus.Features
{
    /// <summary>
    /// This is used for handle ISP Event
    /// </summary>
    public class EventDataOfOld: EventData
    {
        public DateTimeOffset OriginTime { get; set; }
    }
}

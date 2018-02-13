using System;
using Lily.Microservice.Microparts.EventBus.Features;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Contract
{
    public interface IMessagePublisher : IDisposable
    {
        /// <summary>
        /// Gets the name of the publisher.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Initializes message publisher.
        /// </summary>
        /// <param name="name"></param>
        void Initialize(string name);

        /// <summary>
        /// Switches the publisher to send only mode.
        /// </summary>
        void Start();

        /// <summary>
        /// Forces the publisher to stop the send only mode.
        /// </summary>
        void Stop();

        /// <summary>
        /// Publishes the given event to all subscribers.
        /// </summary>
        /// <param name="message">Message to be published</param>
        void Publish(EventData message);
    }
}

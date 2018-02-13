namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.MessageService
{
    public class MessageMap
    {
        /// <summary>
        /// Gets or sets the event type
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Gets or sets the listener type for event type.
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the endpoint name for event type.
        /// </summary>
        public string EndpointName { get; set; }

        /// <summary>
        /// Gets or sets the Timeout for the event in Milliseconds
        /// </summary>
        public double Timeout { get; set; }
    }
}

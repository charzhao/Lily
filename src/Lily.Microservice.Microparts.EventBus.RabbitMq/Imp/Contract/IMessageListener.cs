namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Contract
{
    public interface IMessageListener
    {
        /// <summary>
        /// Gets the name of the message listener.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Initiates the subscriber to message listening mode.
        /// </summary>
        void Start();

        /// <summary>
        /// Forces the subscriber to stop the message listening mode.
        /// </summary>
        void Stop();

        /// <summary>
        /// Subscribe the given event to the current listener.
        /// </summary>
        /// <param name="eventCodeArray">event code list</param>
        void Subscribe(short[] eventCodeArray);

        /// <summary>
        /// Initializes message listener.
        /// </summary>
        /// <param name="name"></param>
        void Initialize(string name);
    }
}

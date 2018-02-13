using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Setting;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.InterfaceImp
{
    internal class MicropartOfEventBusStartup : IMicropartOfEventBusStartup
    {
        private bool _initialized;
        public void Start()
        {
            if (!_initialized)
            {
                _initialized = true;
                InitializeMessaging();
            }
        }
        private void InitializeMessaging()
        {
            MessageSubscribeManager.Instance.Initialize();
            MessageSubscribeManager.Instance.StartAll();
            if (MessagingConfiguration.Instance.SubscribeAllEvents)
            {
                MessageSubscribeManager.Instance.SubscribeAllMapping();
            }

            MessagePublishManager.Instance.Initialize();
            MessagePublishManager.Instance.StartAll();
        }

        public void Stop()
        {
            MessageSubscribeManager.Instance.Dispose();
            MessagePublishManager.Instance.Dispose();
        }
    }
}

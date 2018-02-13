using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Setting;
using Newtonsoft.Json;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.MessageService
{
    public class MessageService 
    {
        public static MessageService Instance = new MessageService();

        private readonly HttpClient _client;
        private readonly string _baseUri;
        private readonly string _serviceName;

        private MessageService()
        {
            _client = new HttpClient();
            _baseUri = MessagingConfiguration.Instance.IspMessageEndPoint;
            _serviceName = MessagingConfiguration.Instance.ServiceRuleName;
            _client.BaseAddress = new Uri(_baseUri);
        }

        public async Task<HttpResponseMessage> GetMapping(RequestTypeEnum type)
        {
            if (_client == null)
                return null;

            if (string.IsNullOrEmpty(_baseUri))
            {
                Trace.Write("baseURI empty");
                throw new Exception("_baseUri is empty");
            }
            return await _client.GetAsync(type + "?serviceName=" + _serviceName);
        }

        public SubscriberMessageMappings GetMessageEndpointMapping()
        {
            SubscriberMessageMappings mappings;
            try
            {
                var responseMsg = GetMapping(RequestTypeEnum.GetSubscriberMappings).Result;
                string jsonMessage;
                using (var responseStream = responseMsg.Content.ReadAsStreamAsync().Result)
                {
                    jsonMessage = new StreamReader(responseStream).ReadToEnd();
                }

                mappings = JsonConvert.DeserializeObject<SubscriberMessageMappings>(jsonMessage);
            }
            catch (Exception exception)
            {
                Trace.Write("Exception in getsubscriber mapping: " + exception.Message);
                throw;
            }

            return mappings;
        }

        public PublisherMessageMappings GetPublisherProviderMapping()
        {
            PublisherMessageMappings mappings;
            try
            {
                var responseMsg = GetMapping(RequestTypeEnum.GetPublisherrMappings).Result;
                string jsonMessage;
                using (var responseStream = responseMsg.Content.ReadAsStreamAsync().Result)
                {
                    jsonMessage = new StreamReader(responseStream).ReadToEnd();
                }

                mappings = JsonConvert.DeserializeObject<PublisherMessageMappings>(jsonMessage);
            }
            catch (Exception exception)
            {
                Trace.Write("Exception in getpublisher mapping: " + exception.Message);
                throw;
            }
            return mappings;
        }
    }

    public enum RequestTypeEnum
    {
        GetSubscriberMappings = 0,
        GetPublisherrMappings = 1
    }
}

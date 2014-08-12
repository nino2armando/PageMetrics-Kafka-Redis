using System;
using System.Configuration;
using KafkaNet;
using KafkaNet.Model;

namespace PageMetrics.KafkaClient
{
    public class MessageBusClient
    {
        private string KafkaServerUri { get; set; }

        public MessageBusClient(string kafkaServerUri = null)
        {
            KafkaServerUri = !string.IsNullOrEmpty(kafkaServerUri) ? kafkaServerUri : ConfigurationManager.AppSettings["Kafka_Server_Uri"];
        }

        public IBrokerRouter GetClientRouter()
        {
            var options = new KafkaOptions(new Uri(KafkaServerUri));
            return new BrokerRouter(options);
        }
    }
}

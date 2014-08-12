using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using PageMetrics.KafkaClient;
using StackExchange.Redis;

namespace PageMetrics
{
    class Program
    {
        static void Main(string[] args)
        {
            ////var options = new KafkaOptions(new Uri("http://localhost:9092"));
            ////var router = new BrokerRouter(options);
            
            //// PRODUCER
            ////var producer = new Producer(router);
            ////producer.SendMessageAsync("test2", new[] {new Message {Value = Guid.NewGuid().ToString()}});

            //// producer using client
            ////var producerusingClient = new JsonProducer(router);
            ////producerusingClient.Publish("test2", new[] {new Message {Key = "hoy",Value = Guid.NewGuid().ToString()}});

            ////CONSUMER
            ////Task.Factory.StartNew(() =>
            ////    {
            ////        var consumer = new Consumer(new ConsumerOptions("test2", router));

            ////        foreach (var data in consumer.Consume())
            ////        {
            ////            Console.WriteLine(data.Value);
            ////        }
            ////    });

            var conn = ConnectionMultiplexer.Connect("192.168.56.102:6379");

            
            Console.ReadKey();
        }
    }
}

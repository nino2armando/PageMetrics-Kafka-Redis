using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using PageMetrics.KafkaClient;
using PageMetrics.PersistentDataStore;
using PageMetrics.PersistentDataStore.Models;
using ServiceStack.Redis;


namespace PageMetrics
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisUri = ConfigurationManager.AppSettings["Redis_Server_Uri"];
            IRedisClientsManager clientManger = new PooledRedisClientManager(redisUri);
            PageRepository pageRepository = new PageRepository(clientManger.GetClient());

            // bin\windows\zookeeper-server-start.bat config\zookeeper.properties
            // bin\windows\kafka-server-start.bat config\server.properties
            // bin\windows\kafka-console-consumer.bat --zookeeper localhost:2181 --topic PageLoadTime --from-beginning
            // kafka-console-producer.bat --broker-list localhost:9092 --topic PageLoadTime


            //// CONSUMER READING OFF THE QUEUE
            //var options = new KafkaOptions(new Uri("http://localhost:9092"));
            //var router = new BrokerRouter(options);

            //var redisClient = new RedisClient("127.0.0.1:6379");
            //var db = redisClient.Instance(1);

            //var consumer = new Consumer(new ConsumerOptions("PageLoadTime", router));
            //var allData = consumer.Consume();
            //Task.Factory.StartNew(() =>
            //    {
            //        int i = 0;
            //        foreach (var data in allData)
            //        {
            //            if (string.IsNullOrEmpty(data.Key))
            //            {
            //                continue;
            //            }
            //            Console.ForegroundColor = ConsoleColor.Green;
            //            Console.WriteLine(string.Format("Reading {0} message => {1}", i, data.Value));
            //            Console.ForegroundColor = ConsoleColor.Yellow;
            //            Console.WriteLine("----------------------------------------------------------");
            //            db.StringSetAsync(data.Key, data.Value.ToString(CultureInfo.InvariantCulture));
            //            i++;
            //        }
            //    });

            var redisData = pageRepository.GetAll();
            DisplayAll(redisData);
            // CONSUMER READING OFF THE QUEUE + REDIS

            var clientSettings = new MessageBusClient();
            var router = clientSettings.GetClientRouter();
            var consumer = new JsonConsumer<PageModel>(new ConsumerOptions("PageLoadTime", router));

            var allData = consumer.Consume();

          
            Task.Factory.StartNew(() =>
                {
                    foreach (var data in allData)
                    {
                        if (string.IsNullOrEmpty(data.Value.Key))
                        {
                            continue;
                        }

                        var page = pageRepository.Store(data.Value);
                        DisplaySingle(page);
                    }
                });

            Console.ReadKey();
        }

        public static void DisplayAll(IList<PageModel> redisData)
        {
            foreach (var pageModel in redisData)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("Reading message with Kafka key => {0} and Redis Id => {1}", pageModel.Key, pageModel.Id));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            }
        }

        public static void DisplaySingle(PageModel model)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Format("Reading message with Kafka key => {0} and Redis Id => {1}", model.Key, model.Id));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using PageMetrics.KafkaClient;
using PageMetrics.PersistentDataStore;
using PageMetrics.PersistentDataStore.Models;
using ServiceStack.Redis;

namespace PageMetrics.API.Controllers
{
    [Authorize]
    public class MetricController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] JObject loadTime)
        {
            var clientSettings = new MessageBusClient();
            var router = clientSettings.GetClientRouter();
            
            var page = new PageModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Key = Guid.NewGuid().ToString("N"),
                Source = new Dictionary<string, string>
                        {
                            {"Request.ApplicationPath", HttpContext.Current.Request.ApplicationPath},
                            {"Request.PhysicalApplicationPath", HttpContext.Current.Request.PhysicalApplicationPath},
                            {"Request.PhysicalPath", HttpContext.Current.Request.PhysicalPath},
                        },
                Metric = new MetricModel
                {
                    Key = MetricType.LoadTime.ToString(),
                    Value = loadTime.ToString()
                },
                Time = DateTime.UtcNow
            };

            Task.Factory.StartNew(() =>
            {
                var producerusingClient = new JsonProducer(router);
                // todo: topics need to be maintained dynamically
                producerusingClient.Publish("PageLoadTime", new List<PageModel> { page });
            });

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public void PostOtherEvents(string eventarg)
        {
            // get the page name
            // determine the type of metric
            // create a payload and fire off

        }
    }
}

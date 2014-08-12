using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PageMetrics.API.Models;
using PageMetrics.KafkaClient;

namespace PageMetrics.API.Controllers
{
    public class MetricController : Controller
    {
        //
        // GET: /Metric/

        public ActionResult Index()
        {
            return View();
        }

        public void PostPageLoad(string loadTime)
        {
            var clientSettings = new MessageBusClient();
            var router = clientSettings.GetClientRouter();
            var producer = new JsonProducer(router);

            var page = new PageModel
                {
                    Id = Guid.NewGuid(),
                    Source = new Dictionary<string, string>
                        {
                            {"Request.ApplicationPath", Request.ApplicationPath},
                            {"Request.PhysicalApplicationPath", Request.PhysicalApplicationPath},
                            {"Request.PhysicalPath", Request.PhysicalPath},
                        },
                    Metric = new MetricModel
                        {
                            Key = MetricType.LoadTime.ToString(),
                            Value = loadTime
                        },
                        Time = DateTime.UtcNow
                };


            Task.Factory.StartNew(() =>
                {
                    var producerusingClient = new JsonProducer(router);
                    // todo: topics need to be maintained dynamically
                    producerusingClient.Publish("PageLoadTime", new List<PageModel> { page });
                });
        }

        public void PostOtherEvents(string eventarg)
        {
            // get the page name
            // determine the type of metric
            // create a payload and fire off

        }
    }
}

using System;
using System.Collections.Generic;

namespace PageMetrics.PersistentDataStore.Models
{
    public class PageModel
    {
        // redis ID
        public string Id { get; set; }
        // Kafka Key
        public string Key { get; set; }
        public Dictionary<string, string> Source { get; set; }
        public MetricModel Metric { get; set; }
        public DateTime Time { get; set; }

        public PageModel()
        {
            Source = new Dictionary<string, string>();
        }
    }
}
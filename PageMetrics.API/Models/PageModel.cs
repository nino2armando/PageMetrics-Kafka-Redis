using System;
using System.Collections.Generic;

namespace PageMetrics.API.Models
{
    public class PageModel
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> Source { get; set; }
        public MetricModel Metric { get; set; }
        public DateTime Time { get; set; }

        public PageModel()
        {
            Source = new Dictionary<string, string>();
        }
    }
}
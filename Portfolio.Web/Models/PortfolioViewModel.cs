using System.Collections.Generic;

namespace Portfolio.Web.Models
{
    public class PortfolioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Asset { get; set; } = new List<string>();
        public List<int> AssetVolume { get; set; } = new List<int>();
        public List<string> BenchIndex { get; set; } = new List<string>();
        public List<double> IndexWeight { get; set; } = new List<double>();
        public List<string> TimePeriod { get; set; } = new List<string>();
        public List<string> PMetrics { get; set; } = new List<string>();
        public List<double> PMetricValues { get; set; } = new List<double>();
        public List<string> BMetrics { get; set; } = new List<string>();
        public List<double> BMetricValues { get; set; } = new List<double>();
        public List<decimal> PortfolioPerformance { get; set; } = new List<decimal>();
        public List<decimal> BenchmarkPerformance { get; set; } = new List<decimal>();
    }
}
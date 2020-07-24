using System.Collections.Generic;
using LiveCharts;

namespace AutoTrading.Laboratory.BackTesting
{
    public class LiveChartModel
    {
        public SeriesCollection SeriesCollection { get; set; }
        public IEnumerable<string> Labels { get; set; }
        public decimal MinY { get; set; }
        public decimal MaxY { get; set; }
        public string Report { get; set; }
    }
}
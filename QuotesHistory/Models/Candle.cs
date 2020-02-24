using System;

namespace QuotesHistory.Models
{
    public class Candle
    {
        public DateTime CloseDateTime { get; set; }
        public decimal OpenValue { get; set; }
        public decimal CloseValue { get; set; }
        public decimal LowValue { get; set; }
        public decimal HighValue { get; set; }
        public int Volume { get; set; }
    }
}
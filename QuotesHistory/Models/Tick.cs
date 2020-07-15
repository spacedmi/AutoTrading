using System;

namespace AutoTrading.QuotesHistory.Models
{
    public class Tick
    {
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
        public int Volume { get; set; }
    }
}
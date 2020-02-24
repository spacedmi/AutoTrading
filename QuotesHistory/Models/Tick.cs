using System;

namespace QuotesHistory.Models
{
    public class Tick
    {
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
        public int Volume { get; set; }
    }
}
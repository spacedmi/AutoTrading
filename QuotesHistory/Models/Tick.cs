using System;

namespace AutoTrading.QuotesHistory.Models
{
    public class Tick
    {
        public Tick(decimal value, int volume, DateTime dateTime)
        {
            DateTime = dateTime;
            Value = value;
            Volume = volume;
        }

        public DateTime DateTime { get; }
        public decimal Value { get; }
        public int Volume { get; }
    }
}
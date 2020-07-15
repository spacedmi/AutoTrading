using System;

namespace AutoTrading.QuotesHistory.Models
{
    public class Candle
    {
        public Candle(decimal openValue, decimal closeValue, decimal lowValue, decimal highValue, int volume, DateTime closeDateTime)
        {
            OpenValue = openValue;
            CloseValue = closeValue;
            LowValue = lowValue;
            HighValue = highValue;
            Volume = volume;
            CloseDateTime = closeDateTime;
        }

        public DateTime CloseDateTime { get; }
        public decimal OpenValue { get; }
        public decimal CloseValue { get; }
        public decimal LowValue { get; }
        public decimal HighValue { get; }
        public int Volume { get; }
    }
}
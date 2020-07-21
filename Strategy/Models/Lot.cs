using System;

namespace AutoTrading.Strategy.Models
{
    public abstract class Lot
    {
        protected Lot(decimal open, DateTimeOffset openTime, int volume)
        {
            OpenTime = openTime;
            Open = open;
            Volume = volume;
        }

        public decimal Open { get; }
        public decimal? Close { get; private set; }
        public DateTimeOffset OpenTime { get; }
        public DateTimeOffset? CloseTime { get; private set; }
        public int Volume { get; }
        public abstract decimal? Profit { get; }

        public void CloseLot(decimal closeValue, DateTimeOffset closeTime)
        {
            Close = closeValue;
            CloseTime = closeTime;
        }
    }
}
using System;
using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.Strategy.Models
{
    public abstract class Lot
    {
        protected Lot(Tick tick, int volume)
        {
            OpenTime = tick.DateTime;
            Open = tick.Value;
            OpenTick = tick;
            Volume = volume;
        }

        public decimal Open { get; }
        public decimal? Close { get; private set; }
        public DateTimeOffset OpenTime { get; }
        public DateTimeOffset? CloseTime { get; private set; }
        public Tick OpenTick { get; set; }
        public Tick CloseTick { get; set; }
        public int Volume { get; }
        public abstract decimal? Profit { get; }

        public void CloseLot(Tick tick)
        {
            Close = tick.Value;
            CloseTime = tick.DateTime;
            CloseTick = tick;
        }
    }
}
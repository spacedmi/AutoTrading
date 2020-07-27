using System;
using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.Strategy.Models
{
    public class LongLot : Lot
    {
        public LongLot(Tick tick, int volume) : base(tick, volume)
        {
        }

        public override decimal? Profit => (Close - Open) * Volume;
    }
}
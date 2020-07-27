using System;
using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.Strategy.Models
{
    public class ShortLot : Lot
    {
        public ShortLot(Tick tick, int volume) : base(tick, volume)
        {
        }

        public override decimal? Profit => (Open - Close) * Volume;
    }
}
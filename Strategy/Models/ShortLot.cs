using System;

namespace AutoTrading.Strategy.Models
{
    public class ShortLot : Lot
    {
        public ShortLot(decimal open, DateTimeOffset openTime, int volume) : base(open, openTime, volume)
        {
        }

        public override decimal? Profit => (Open - Close) * Volume;
    }
}
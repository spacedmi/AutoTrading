using System;

namespace AutoTrading.Strategy.Models
{
    public class LongLot : Lot
    {
        public LongLot(decimal open, DateTimeOffset openTime, int volume) : base(open, openTime, volume)
        {
        }

        public override decimal? Profit => (Close - Open) * Volume;
    }
}
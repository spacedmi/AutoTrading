namespace AutoTrading.Strategy.Models
{
    public class LongLot : Lot
    {
        public LongLot(decimal open, int volume) : base(open, volume)
        {
        }

        public override decimal? Profit => (Close - Open) * Volume;
    }
}
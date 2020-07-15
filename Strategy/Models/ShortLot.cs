namespace AutoTrading.Strategy.Models
{
    public class ShortLot : Lot
    {
        public ShortLot(decimal open, int volume) : base(open, volume)
        {
        }

        public override decimal? Profit => (Open - Close) * Volume;
    }
}
namespace AutoTrading.Strategy.Models
{
    public abstract class Lot
    {
        protected Lot(decimal open, int volume)
        {
            Open = open;
            Volume = volume;
        }

        public decimal Open { get; }
        public decimal? Close { get; set; }
        public int Volume { get; }
        public abstract decimal? Profit { get; }
    }
}
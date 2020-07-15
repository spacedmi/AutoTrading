using System.Collections.Generic;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.Strategy.Models;

namespace AutoTrading.Strategy
{
    public interface IStrategy
    {
        IEnumerable<Lot> Lots { get; }
        void LoadHistory(IEnumerable<Candle> historyCandles);
        void OnTick(Tick tick);
    }
}
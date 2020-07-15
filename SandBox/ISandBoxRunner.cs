using System.Collections.Generic;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.SandBox.Models;
using AutoTrading.Strategy;

namespace AutoTrading.SandBox
{
    public interface ISandBoxRunner
    {
        Report RunStrategy(IEnumerable<Candle> historyCandles, IStrategy strategy, IEnumerable<Tick> ticks);
    }
}
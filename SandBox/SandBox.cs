﻿using System.Collections.Generic;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.SandBox.Models;
using AutoTrading.Strategy;

namespace AutoTrading.SandBox
{
    public class SandBox : ISandBox
    {
        public Report TestStrategy(IEnumerable<Candle> historyCandles, IStrategy strategy, IEnumerable<Tick> ticks)
        {
            strategy.LoadHistory(historyCandles);
            
            foreach (var tick in ticks)
            {
                strategy.OnTick(tick);
            }
            
            return new Report(strategy.Lots);
        }
    }
}
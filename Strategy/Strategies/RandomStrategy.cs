﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.Strategy.Models;

namespace AutoTrading.Strategy.Strategies
{
    /// <summary>
    /// Random open short or long lot and close previous lot per tick. Only one lot can be opened at the moment
    /// </summary>
    public class RandomStrategy : IStrategy
    {
        private readonly List<Lot> lots;
        private readonly List<Candle> history;
        private readonly Random random;

        public RandomStrategy()
        {
            lots = new List<Lot>();
            history = new List<Candle>();
            random = new Random();
        }

        public IReadOnlyCollection<Lot> Lots => lots;
        
        public void LoadHistory(IEnumerable<Candle> historyCandles)
        {
            history.AddRange(historyCandles);
        }

        public void OnTick(Tick tick)
        {
            var openedLot = lots.FirstOrDefault(x => !x.Close.HasValue);
            if (openedLot != null)
            {
                openedLot.Close = tick.Value;
            }
            
            if (random.Next(100) % 100 == 0)
            {
                lots.Add(new LongLot(open: tick.Value, volume: 100)); // TODO use account money
            }
            else
            {
                lots.Add(new ShortLot(open: tick.Value, volume: 100)); // TODO use account money
            }
        }
    }
}
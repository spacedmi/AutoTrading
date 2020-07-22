using System;
using System.Collections.Generic;
using System.Linq;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.Strategy.Models;

namespace AutoTrading.Strategy.Strategies
{
    /// <summary>
    /// Random open short or long lot and close previous lot. Only one lot can be opened at the moment
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

        public IEnumerable<Lot> Lots => lots;
        
        public void LoadHistory(IEnumerable<Candle> historyCandles)
        {
            history.AddRange(historyCandles);
        }

        public void OnTick(Tick tick)
        {
            // Random ignore tick
            if (random.Next(100) % 2 == 0)
            {
                return;
            }

            var openedLot = lots.FirstOrDefault(x => !x.Close.HasValue);
            if (openedLot != null)
            {
                openedLot.CloseLot(tick.Value, tick.DateTime);
            }
            
            // Random continue
            if (random.Next(100) % 2 == 0)
            {
                return;
            }
            
            if (random.Next(100) % 2 == 0)
            {
                lots.Add(new LongLot(open: tick.Value, tick.DateTime, volume: 100)); // TODO use account money
            }
            else
            {
                lots.Add(new ShortLot(open: tick.Value, tick.DateTime, volume: 100)); // TODO use account money
            }
        }
    }
}
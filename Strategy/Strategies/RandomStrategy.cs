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
    public class RandomStrategy : IStrategy, IDisposable
    {
        private readonly int randomParameter;
        private readonly List<Lot> lots;
        private readonly List<Candle> history;
        private readonly List<IObserver<Lot>> observers;
        private readonly Random random;

        public RandomStrategy(int randomParameter)
        {
            this.randomParameter = randomParameter;
            lots = new List<Lot>();
            history = new List<Candle>();
            observers = new List<IObserver<Lot>>();
            random = new Random();
        }

        public IEnumerable<Lot> Lots => lots;
        
        public void LoadHistory(IEnumerable<Candle> historyCandles)
        {
            history.AddRange(historyCandles);
        }

        public void OnTick(Tick tick)
        {
            try
            {
                // Random ignore tick
                if (random.Next(randomParameter) % randomParameter != 0)
                {
                    return;
                }

                var lotToClose = lots.FirstOrDefault(x => !x.Close.HasValue);
                if (lotToClose != null)
                {
                    lotToClose.CloseLot(tick);
                    observers.ForEach(x => x.OnNext(lotToClose));
                }
            
                // Random continue
                if (random.Next(randomParameter) % randomParameter != 0)
                {
                    return;
                }
            
                if (random.Next(100) % 2 == 0)
                {
                    lots.Add(new LongLot(tick, volume: 100)); // TODO use account money
                }
                else
                {
                    lots.Add(new ShortLot(tick, volume: 100)); // TODO use account money
                }
            }
            catch (Exception e)
            {
                observers.ForEach(x => x.OnError(e));
            }
        }

        public IDisposable Subscribe(IObserver<Lot> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return this;
        }

        public void Dispose()
        {
            observers.Clear();
        }
    }
}
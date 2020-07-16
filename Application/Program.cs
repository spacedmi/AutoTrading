using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoTrading.QuotesHistory;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.SandBox;
using AutoTrading.Strategy.Strategies;

namespace AutoTrading.Application
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var historyProvider = new FinamQuotesHistoryProvider(
                new FinamTicksParser(), 
                new FinamCandlesParser(),
                new FinamQuotesHistoryClient(
                    new HistoryConfiguration()));
            
            var ticksHistory = await historyProvider.GetTicksHistory(
                "GAZP",
                new DateTime(2020, 2, 20, 0, 0, 0),
                new DateTime(2020, 2, 21, 0, 0, 0),
                CancellationToken.None);
            
            var candlesHistory = await historyProvider.GetCandlesHistory(
                "GAZP",
                TimeInterval.Minutes15,
                new DateTime(2020, 2, 20, 0, 0, 0),
                new DateTime(2020, 2, 21, 0, 0, 0),
                CancellationToken.None);

            var sandBoxRunner = new SandBoxRunner();
            var report = sandBoxRunner.RunStrategy(
                new List<Candle>(),
                new RandomStrategy(),
                candlesHistory.Candles.Select(x => new Tick(value: x.CloseValue, x.Volume, x.CloseDateTime)));
        }
    }
}
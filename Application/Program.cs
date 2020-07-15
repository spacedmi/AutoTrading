using System;
using System.Threading;
using System.Threading.Tasks;
using AutoTrading.QuotesHistory;
using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.Application
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // TODO use autofac
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

            var sandBox = new SandBox.SandBox();
        }
    }
}
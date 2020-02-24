using System;
using System.Threading;
using System.Threading.Tasks;
using QuotesHistory;
using QuotesHistory.Models;
using TestApp;

namespace Application
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
        }
    }
}
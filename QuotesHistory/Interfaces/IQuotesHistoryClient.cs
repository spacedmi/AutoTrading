using System;
using System.Threading;
using System.Threading.Tasks;
using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.QuotesHistory.Interfaces
{
    public interface IQuotesHistoryClient
    {
        Task<string> GetTicksHistory(
            string ticker,
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken);
        
        Task<string> GetCandlesHistory(
            string ticker,
            TimeInterval timeInterval,
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken);
    }
}
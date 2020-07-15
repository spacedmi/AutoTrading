using System;
using System.Threading;
using System.Threading.Tasks;
using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.QuotesHistory.Interfaces
{
    public interface IQuotesHistoryProvider
    {
        Task<QuotesTicksHistoryModel> GetTicksHistory(
            string ticker,
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken);
        
        Task<QuotesCandlesHistoryModel> GetCandlesHistory(
            string ticker,
            TimeInterval timeInterval,
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken);
    }
}
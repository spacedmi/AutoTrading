using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using QuotesHistory.Models;

namespace QuotesHistory.Interfaces
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
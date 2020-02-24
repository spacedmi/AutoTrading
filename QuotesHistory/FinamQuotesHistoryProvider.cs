using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using QuotesHistory.Interfaces;
using QuotesHistory.Models;

namespace QuotesHistory
{
    public class FinamQuotesHistoryProvider : IQuotesHistoryProvider
    {
        private readonly ITicksParser ticksParser;
        private readonly ICandlesParser candlesParser;
        private readonly IQuotesHistoryClient quotesHistoryClient;

        public FinamQuotesHistoryProvider(ITicksParser ticksParser,
                                          ICandlesParser candlesParser,
                                          IQuotesHistoryClient quotesHistoryClient)
        {
            this.ticksParser = ticksParser;
            this.candlesParser = candlesParser;
            this.quotesHistoryClient = quotesHistoryClient;
        }
        
        public async Task<QuotesTicksHistoryModel> GetTicksHistory(
            string ticker, 
            DateTime dateFrom, 
            DateTime dateTo,
            CancellationToken cancellationToken)
        {
            var response = await quotesHistoryClient.GetTicksHistory(ticker,
                dateFrom,
                dateTo,
                cancellationToken);
            using var stringReader = new StringReader(response);
            
            string line;
            var ticks = new List<Tick>();
            while ((line = await stringReader.ReadLineAsync()) != null)
            {
                ticks.Add(ticksParser.ParseTick(line));
            }
            
            return new QuotesTicksHistoryModel
            {
                Ticks = ticks, 
            };
        }

        public async Task<QuotesCandlesHistoryModel> GetCandlesHistory(
            string ticker, 
            TimeInterval timeInterval,
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken)
        {
            var response = await quotesHistoryClient.GetCandlesHistory(ticker,
                timeInterval,
                dateFrom,
                dateTo,
                cancellationToken);
            using var stringReader = new StringReader(response);
            
            string line;
            var candles = new List<Candle>();
            while ((line = await stringReader.ReadLineAsync()) != null)
            {
                candles.Add(candlesParser.ParseCandle(line));
            }
            
            return new QuotesCandlesHistoryModel
            {
                TimeInterval = timeInterval,
                Candles = candles,
            };
        }
    }
}
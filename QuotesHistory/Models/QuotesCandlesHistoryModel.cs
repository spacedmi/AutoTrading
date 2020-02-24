using System.Collections.Generic;

namespace QuotesHistory.Models
{
    public class QuotesCandlesHistoryModel
    {
        public TimeInterval TimeInterval { get; set; }
        public ICollection<Candle> Candles { get; set; }
    }
}
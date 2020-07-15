using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.QuotesHistory.Interfaces
{
    public interface ICandlesParser
    {
        Candle ParseCandle(string input);
    }
}
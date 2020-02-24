using QuotesHistory.Models;

namespace QuotesHistory.Interfaces
{
    public interface ICandlesParser
    {
        Candle ParseCandle(string input);
    }
}
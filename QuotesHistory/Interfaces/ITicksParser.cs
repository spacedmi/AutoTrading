using AutoTrading.QuotesHistory.Models;

namespace AutoTrading.QuotesHistory.Interfaces
{
    public interface ITicksParser
    {
        Tick ParseTick(string input);
    }
}
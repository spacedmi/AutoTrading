using QuotesHistory.Models;

namespace QuotesHistory.Interfaces
{
    public interface ITicksParser
    {
        Tick ParseTick(string input);
    }
}
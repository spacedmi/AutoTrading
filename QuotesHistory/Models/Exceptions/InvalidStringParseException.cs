using System;

namespace AutoTrading.QuotesHistory.Models.Exceptions
{
    public class InvalidStringParseException : Exception
    {
        public InvalidStringParseException(string input, int parametersCount) :
            base($"Input string \"{input}\" should consist of {parametersCount} parameters separated by comma")
        {
        }
    }
}
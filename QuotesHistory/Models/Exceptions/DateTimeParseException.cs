using System;

namespace AutoTrading.QuotesHistory.Models.Exceptions
{
    public class DateTimeParseException : Exception
    {
        public DateTimeParseException(string input) :
            base($"Cannot parse datetime value: {input}")
        {
        }
    }
}
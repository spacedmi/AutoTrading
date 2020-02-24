using System;

namespace QuotesHistory.Models.Exceptions
{
    public class ValueParseException : Exception
    {
        public ValueParseException(string input) :
            base($"Cannot parse decimal value: {input}")
        {
        }
    }
}
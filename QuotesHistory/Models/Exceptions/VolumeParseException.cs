using System;

namespace QuotesHistory.Models.Exceptions
{
    public class VolumeParseException : Exception
    {
        public VolumeParseException(string input) :
            base($"Cannot parse volume int value: {input}")
        {
        }
    }
}
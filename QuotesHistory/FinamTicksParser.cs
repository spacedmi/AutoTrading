using System;
using System.Globalization;
using QuotesHistory.Interfaces;
using QuotesHistory.Models;
using QuotesHistory.Models.Exceptions;

namespace QuotesHistory
{
    public class FinamTicksParser : ITicksParser
    {
        private int parametersCount = 4;
        
        public Tick ParseTick(string input)
        {
            var split = input?.Split(',');
            if (string.IsNullOrEmpty(input) || split.Length != parametersCount)
            {
                throw new InvalidStringParseException(input, parametersCount);
            }
            
            var dateTimeString = split[0] + split[1];
            var valueString = split[2];
            var volumeString = split[3];
            
            if (!DateTime.TryParseExact(dateTimeString, 
                "yyyyMMddHHmmss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal, 
                out var dateTime))
            {
                throw new DateTimeParseException(dateTimeString);
            }
            
            if (!decimal.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                throw new ValueParseException(valueString);
            }
            
            if (!int.TryParse(volumeString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var volume))
            {
                throw new VolumeParseException(volumeString);
            }
            
            return new Tick
            {
                Value = value,
                Volume = volume,
                DateTime = dateTime,
            };
        }
    }
}
using System;
using System.Globalization;
using AutoTrading.QuotesHistory.Interfaces;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.QuotesHistory.Models.Exceptions;

namespace AutoTrading.QuotesHistory
{
    public class FinamCandlesParser : ICandlesParser
    {
        private int parametersCount = 7;
        public Candle ParseCandle(string input)
        {
            var split = input?.Split(',');
            if (string.IsNullOrEmpty(input) || split.Length != parametersCount)
            {
                throw new InvalidStringParseException(input, parametersCount);
            }
            
            var dateTimeString = split[0] + split[1];
            var openString = split[2];
            var highString = split[3];
            var lowString = split[4];
            var closeString = split[5];
            var volumeString = split[6];
            
            if (!DateTime.TryParseExact(dateTimeString, 
                "yyyyMMddHHmmss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal, 
                out var dateTime))
            {
                throw new DateTimeParseException(dateTimeString);
            }
            
            if (!decimal.TryParse(openString, NumberStyles.Float, CultureInfo.InvariantCulture, out var openValue))
            {
                throw new ValueParseException(openString);
            }
            if (!decimal.TryParse(highString, NumberStyles.Float, CultureInfo.InvariantCulture, out var highValue))
            {
                throw new ValueParseException(highString);
            }
            if (!decimal.TryParse(lowString, NumberStyles.Float, CultureInfo.InvariantCulture, out var lowValue))
            {
                throw new ValueParseException(lowString);
            }
            if (!decimal.TryParse(closeString, NumberStyles.Float, CultureInfo.InvariantCulture, out var closeValue))
            {
                throw new ValueParseException(closeString);
            }
            
            if (!int.TryParse(volumeString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var volume))
            {
                throw new VolumeParseException(volumeString);
            }
            
            return new Candle
            {
                OpenValue = openValue,
                HighValue = highValue,
                LowValue = lowValue,
                CloseValue = closeValue,
                Volume = volume,
                CloseDateTime = dateTime,
            };
        }
    }
}
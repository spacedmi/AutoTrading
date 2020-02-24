using System;
using FluentAssertions;
using QuotesHistory.Models.Exceptions;
using Xunit;

namespace QuotesHistory.Tests
{
    public class FinamCandlesParserShould
    {
        private FinamCandlesParser finamCandlesParser;

        public FinamCandlesParserShould()
        {
            finamCandlesParser = new FinamCandlesParser();
        }
        
        [Fact]
        public void ParseLine()
        {
            var inputLine = "20200220,100100,232.5500000,232.9800000,232.0000000,232.5300000,449000";
            var parsedTick = finamCandlesParser.ParseCandle(inputLine);

            parsedTick.OpenValue.Should().Be(232.55M);
            parsedTick.HighValue.Should().Be(232.98M);
            parsedTick.LowValue.Should().Be(232M);
            parsedTick.CloseValue.Should().Be(232.53M);
            parsedTick.Volume.Should().Be(449000);
            parsedTick.CloseDateTime.Year.Should().Be(2020);
            parsedTick.CloseDateTime.Month.Should().Be(2);
            parsedTick.CloseDateTime.Day.Should().Be(20);
            parsedTick.CloseDateTime.Hour.Should().Be(10);
            parsedTick.CloseDateTime.Minute.Should().Be(1);
            parsedTick.CloseDateTime.Second.Should().Be(0);
        }
        
        [Theory]
        [InlineData("20200220,100100,,232.9800000,232.0000000,232.5300000,449000", "")]
        [InlineData("20200220,100100,232.5500000,,232.0000000,232.5300000,449000", "")]
        [InlineData("20200220,100100,232.5500000,232.9800000,,232.5300000,449000", "")]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,,449000", "")]
        [InlineData("20200220,100100,232?5500000,232.9800000,232.0000000,232.5300000,449000", "232?5500000")]
        [InlineData("20200220,100100,232.5500000,232-9800000,232.0000000,232.5300000,449000", "232-9800000")]
        [InlineData("20200220,100100,232.5500000,232.9800000,%,232.5300000,449000", "%")]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,+-44,449000", "+-44")]
        public void ThrowValueParseException(string inputLine, string expectedParsedValue)
        {
            var action = new Action(() => finamCandlesParser.ParseCandle(inputLine));
            action.Should().Throw<ValueParseException>()
                           .WithMessage($"Cannot parse decimal value: {expectedParsedValue}");
        }
        
        [Theory]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,232.5300000,20.5", "20.5")]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,232.5300000,", "")]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,232.5300000,111111111111111111111111111111111111111111111111", 
            "111111111111111111111111111111111111111111111111")]
        public void ThrowVolumeParseException(string inputLine, string expectedParsedValue)
        {
            var action = new Action(() => finamCandlesParser.ParseCandle(inputLine));
            action.Should().Throw<VolumeParseException>()
                .WithMessage($"Cannot parse volume int value: {expectedParsedValue}");
        }
        
        [Theory]
        [InlineData("20200220,,232.5500000,232.9800000,232.0000000,232.5300000,4490000", "20200220")]
        [InlineData("20200220,?,232.5500000,232.9800000,232.0000000,232.5300000,449000", "20200220?")]
        [InlineData(",130941,232.5500000,232.9800000,232.0000000,232.5300000,449000", "130941")]
        [InlineData(",,232.5500000,232.9800000,232.0000000,232.5300000,449000", "")]
        [InlineData("-,130941,232.5500000,232.9800000,232.0000000,232.5300000,449000", "-130941")]
        [InlineData("202002200,130941,232.5500000,232.9800000,232.0000000,232.5300000,449000", "202002200130941")]
        [InlineData("20200220,1309410,232.5500000,232.9800000,232.0000000,232.5300000,449000", "202002201309410")]
        [InlineData("20200220,250941,232.5500000,232.9800000,232.0000000,232.5300000,449", "20200220250941")]
        [InlineData("20200220,240941,232.5500000,232.9800000,232.0000000,232.5300000,449", "20200220240941")]
        [InlineData("20200220,236041,232.5500000,232.9800000,232.0000000,232.5300000,449", "20200220236041")]
        [InlineData("20200220,235960,232.5500000,232.9800000,232.0000000,232.5300000,449", "20200220235960")]
        public void ThrowDateTimeParseException(string inputLine, string expectedParsedValue)
        {
            var action = new Action(() => finamCandlesParser.ParseCandle(inputLine));
            action.Should().Throw<DateTimeParseException>()
                .WithMessage($"Cannot parse datetime value: {expectedParsedValue}");
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,232.5300000")]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,232.5300000,449000,1")]
        public void ThrowInvalidStringException(string inputLine)
        {
            var action = new Action(() => finamCandlesParser.ParseCandle(inputLine));
            action.Should().Throw<InvalidStringParseException>()
                .WithMessage($"Input string \"{inputLine}\" should consist of 7 parameters separated by comma");
        }
    }
}
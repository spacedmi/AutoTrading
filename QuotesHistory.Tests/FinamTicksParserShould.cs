using System;
using FluentAssertions;
using QuotesHistory.Models.Exceptions;
using Xunit;

namespace QuotesHistory.Tests
{
    public class FinamTicksParserShould
    {
        private FinamTicksParser finamTicksParser;

        public FinamTicksParserShould()
        {
            finamTicksParser = new FinamTicksParser();
        }
        
        [Fact]
        public void ParseLine()
        {
            var inputLine = "20200220,130941,232.510000000,20";
            var parsedTick = finamTicksParser.ParseTick(inputLine);

            parsedTick.Value.Should().Be(232.51M);
            parsedTick.Volume.Should().Be(20);
            parsedTick.DateTime.Year.Should().Be(2020);
            parsedTick.DateTime.Month.Should().Be(2);
            parsedTick.DateTime.Day.Should().Be(20);
            parsedTick.DateTime.Hour.Should().Be(13);
            parsedTick.DateTime.Minute.Should().Be(9);
            parsedTick.DateTime.Second.Should().Be(41);
        }
        
        [Theory]
        [InlineData("20200220,130941,232-510000000,20", "232-510000000")]
        [InlineData("20200220,130941,232<510000000,20", "232<510000000")]
        [InlineData("20200220,130941,,20", "")]
        [InlineData("20200220,130941,a,20", "a")]
        public void ThrowValueParseException(string inputLine, string expectedParsedValue)
        {
            var action = new Action(() => finamTicksParser.ParseTick(inputLine));
            action.Should().Throw<ValueParseException>()
                           .WithMessage($"Cannot parse decimal value: {expectedParsedValue}");
        }
        
        [Theory]
        [InlineData("20200220,130941,232.510000000,20.5", "20.5")]
        [InlineData("20200220,130941,232.510000000,", "")]
        [InlineData("20200220,130941,232.510000000,111111111111111111111111111111111111111111111111", 
            "111111111111111111111111111111111111111111111111")]
        public void ThrowVolumeParseException(string inputLine, string expectedParsedValue)
        {
            var action = new Action(() => finamTicksParser.ParseTick(inputLine));
            action.Should().Throw<VolumeParseException>()
                .WithMessage($"Cannot parse volume int value: {expectedParsedValue}");
        }
        
        [Theory]
        [InlineData("20200220,,232.510000000,20", "20200220")]
        [InlineData("20200220,?,232.510000000,20", "20200220?")]
        [InlineData(",130941,232.510000000,20", "130941")]
        [InlineData("-,130941,232.510000000,20", "-130941")]
        [InlineData(",,232.510000000,20", "")]
        [InlineData("202002200,130941,232.510000000,20", "202002200130941")]
        [InlineData("20200220,1309410,232.510000000,20", "202002201309410")]
        [InlineData("20200220,250941,232.510000000,20", "20200220250941")]
        [InlineData("20200220,240941,232.510000000,20", "20200220240941")]
        [InlineData("20200220,236041,232.510000000,20", "20200220236041")]
        [InlineData("20200220,235960,232.510000000,20", "20200220235960")]
        public void ThrowDateTimeParseException(string inputLine, string expectedParsedValue)
        {
            var action = new Action(() => finamTicksParser.ParseTick(inputLine));
            action.Should().Throw<DateTimeParseException>()
                .WithMessage($"Cannot parse datetime value: {expectedParsedValue}");
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("20200220,232.510000000,20")]
        [InlineData("20200220,100100,232.5500000,232.9800000,232.0000000,232.5300000,449000")]
        public void ThrowInvalidStringException(string inputLine)
        {
            var action = new Action(() => finamTicksParser.ParseTick(inputLine));
            action.Should().Throw<InvalidStringParseException>()
                .WithMessage($"Input string \"{inputLine}\" should consist of 4 parameters separated by comma");
        }
    }
}
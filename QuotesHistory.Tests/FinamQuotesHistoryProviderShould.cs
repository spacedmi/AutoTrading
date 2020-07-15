using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoTrading.QuotesHistory.Interfaces;
using AutoTrading.QuotesHistory.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace AutoTrading.QuotesHistory.Tests
{
    public class FinamQuotesHistoryProviderShould
    {
        private Mock<ITicksParser> ticksParserMock;
        private Mock<ICandlesParser> candlesParserMock;
        private Mock<IQuotesHistoryClient> quotesHistoryClientMock;
        private FinamQuotesHistoryProvider quotesHistoryProvider;

        public FinamQuotesHistoryProviderShould()
        {
            ticksParserMock = new Mock<ITicksParser>();
            candlesParserMock = new Mock<ICandlesParser>();
            quotesHistoryClientMock = new Mock<IQuotesHistoryClient>();
            quotesHistoryProvider = new FinamQuotesHistoryProvider(
                ticksParserMock.Object,
                candlesParserMock.Object,
                quotesHistoryClientMock.Object);            
        }
        
        [Fact]
        public async Task GetTicksHistory()
        {
            var ticker = "YNDX";
            var dateFrom = new DateTime(2020, 2, 20, 0, 0, 0);
            var dateTo = new DateTime(2020, 2, 21, 0, 0, 0);
            var responseString = "20200220,000000,232.5500000,33547100";
            var parsedTick = new Tick();
            var cancellationToken = new CancellationToken();
            
            ticksParserMock.Setup(x => x.ParseTick(responseString))
                           .Returns(parsedTick);
            quotesHistoryClientMock.Setup(x => x.GetTicksHistory(ticker, dateFrom, dateTo, cancellationToken))
                                   .ReturnsAsync(responseString);

            var history = await quotesHistoryProvider.GetTicksHistory(ticker, dateFrom, dateTo, cancellationToken);

            history.Should().NotBeNull();
            history.Ticks.Should().HaveCount(1);
            history.Ticks.First().Should().Be(parsedTick);
            
            quotesHistoryClientMock.Verify(x => x.GetTicksHistory(ticker, dateFrom, dateTo, cancellationToken), Times.Once);
            quotesHistoryClientMock.VerifyNoOtherCalls();
            
            ticksParserMock.Verify(x => x.ParseTick(responseString));
            ticksParserMock.VerifyNoOtherCalls();
            candlesParserMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async Task GetCandlesHistory()
        {
            var ticker = "YNDX";
            var interval = TimeInterval.Minutes30;
            var dateFrom = new DateTime(2020, 2, 20, 0, 0, 0);
            var dateTo = new DateTime(2020, 2, 21, 0, 0, 0);
            var responseString = "20200220,000000,232.5500000,235.7500000,232.0000000,234.6000000,33547100";
            var parsedCandle = new Candle();
            var cancellationToken = new CancellationToken();
            
            candlesParserMock.Setup(x => x.ParseCandle(responseString))
                                          .Returns(parsedCandle);
            quotesHistoryClientMock.Setup(x => x.GetCandlesHistory(ticker, interval, dateFrom, dateTo, cancellationToken))
                .ReturnsAsync(responseString);

            var history = await quotesHistoryProvider.GetCandlesHistory(ticker, interval, dateFrom, dateTo, cancellationToken);

            history.Should().NotBeNull();
            history.Candles.Should().HaveCount(1);
            history.Candles.First().Should().Be(parsedCandle);
            
            quotesHistoryClientMock.Verify(x => x.GetCandlesHistory(ticker, interval, dateFrom, dateTo, cancellationToken), Times.Once);
            quotesHistoryClientMock.VerifyNoOtherCalls();
            
            candlesParserMock.Verify(x => x.ParseCandle(responseString));
            candlesParserMock.VerifyNoOtherCalls();
            ticksParserMock.VerifyNoOtherCalls();
        }
    }
}
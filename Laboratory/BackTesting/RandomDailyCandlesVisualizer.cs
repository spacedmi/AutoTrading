using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using AutoTrading.Laboratory.Configuration;
using AutoTrading.QuotesHistory;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.SandBox;
using AutoTrading.Strategy.Models;
using AutoTrading.Strategy.Strategies;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace AutoTrading.Laboratory.BackTesting
{
    public class RandomDailyCandlesVisualizer : IDailyStrategyVisualizer
    {
        public async Task<LiveChartModel> Visualize(DateTime date)
        {
            var historyProvider = new FinamQuotesHistoryProvider(
                    new FinamTicksParser(), 
                    new FinamCandlesParser(),
                    new FinamQuotesHistoryClient(
                        new HistoryConfiguration()));
            
                var candlesHistory = await historyProvider.GetCandlesHistory(
                    "GAZP",
                    TimeInterval.Minutes10,
                    date.Date,
                    date.Date.AddDays(1).AddSeconds(-1),
                    CancellationToken.None);

                var candles = candlesHistory.Candles.ToList();
                var sandBoxRunner = new SandBoxRunner();
                var report = sandBoxRunner.RunStrategy(
                    new List<Candle>(),
                    new RandomStrategy(),
                    candles.Select(x => new Tick(value: x.CloseValue, x.Volume, x.CloseDateTime)));

                var candlesToShow = new ChartValues<OhlcPoint>();
                candlesToShow.AddRange(candles.Select(x => 
                    new OhlcPoint((double) x.OpenValue, (double) x.HighValue, (double) x.LowValue, (double) x.CloseValue)));

                var lots = report.CloseLots.Select(lot =>
                {
                    var openPosition = candles.FindIndex(c => c.CloseDateTime == lot.OpenTime);
                    var closePosition = candles.FindIndex(c => c.CloseDateTime == lot.CloseTime);
                    var brush = lot is LongLot ? Brushes.Blue : Brushes.Yellow;
                    var pointGeometry = lot is LongLot 
                        ? Geometry.Parse("M 0 0 L 4 -4 L 8 0 Z")
                        : Geometry.Parse("M 0 0 L 4 4 L 8 0 Z");
                    return new LineSeries
                    {
                        Values = new ChartValues<ObservablePoint>
                        {
                            new ObservablePoint(openPosition, (double) lot.Open), 
                            new ObservablePoint(closePosition, (double) lot.Close),
                        },
                        Fill = Brushes.Transparent,
                        Stroke = brush,
                        PointForeground  = brush,
                        PointGeometry = pointGeometry,
                        PointGeometrySize = 10,
                    };
                });

                var candlesDates = candlesHistory.Candles.Select(x => x.CloseDateTime.ToString("HH:mm:ss"));
                var seriesCollection = new SeriesCollection
                {
                    new CandleSeries
                    {
                        Values = candlesToShow,
                    },
                };
                seriesCollection.AddRange(lots);
                return new LiveChartModel
                {
                    SeriesCollection = seriesCollection,
                    Labels = candlesDates,
                    MinY = candlesHistory.Candles.Min(x => x.CloseValue),
                    MaxY = candlesHistory.Candles.Max(x => x.CloseValue),
                    Report = report.ToString(),
                };
        }
    }
}
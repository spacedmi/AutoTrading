using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using AutoTrading.Laboratory.Configuration;
using AutoTrading.QuotesHistory;
using AutoTrading.QuotesHistory.Models;
using AutoTrading.SandBox;
using AutoTrading.SandBox.Models;
using AutoTrading.Strategy.Models;
using AutoTrading.Strategy.Strategies;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace AutoTrading.Laboratory.BackTesting.Random
{
    public class RandomDailyTicksVisualizer : IObserver<Lot>, IDailyStrategyVisualizer
    {
        private readonly TextBlock textBlock;
        private SeriesCollection seriesCollection;
        private List<Tick> ticks;

        public RandomDailyTicksVisualizer(TextBlock textBlock)
        {
            this.textBlock = textBlock;
        }

        public async Task<LiveChartModel> Visualize(DateTime date)
        {
            var ticksToShow = new ChartValues<decimal>();
            seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = ticksToShow,
                    PointGeometrySize = 1,
                    PointForeground = Brushes.Black,
                    Foreground = Brushes.Black,
                },
            };
            
            var historyProvider = new FinamQuotesHistoryProvider(
                new FinamTicksParser(),
                new FinamCandlesParser(),
                new FinamQuotesHistoryClient(
                    new HistoryConfiguration()));
            
            Log("Load ticks");
            ticks = (await historyProvider.GetTicksHistory(
                "GAZP",
                date.Date,
                date.Date.AddDays(1),
                CancellationToken.None)).Ticks.ToList();
            Log($"{ticks.Count} ticks loaded");

            textBlock.Dispatcher.InvokeAsync(async () =>
            {
                using var strategy = new RandomStrategy(10);
                strategy.Subscribe(this);
                for (var i = 0; i < ticks.Count; i++)
                {
                    Log($"Tick {i} of {ticks.Count}");
                    var tick = ticks[i];
                    strategy.OnTick(tick);
                    ticksToShow.Add(tick.Value);
                    await Task.Delay(20);
                }
                
                var report = new Report(strategy);
                Log(report.ToString());
            });

            var ticksDates = ticks.Select(x => x.DateTime.ToString("HH:mm:ss")).ToList();
            return new LiveChartModel
            {
                SeriesCollection = seriesCollection,
                Labels = ticksDates,
                MinY = ticks.Min(x => x.Value),
                MaxY = ticks.Max(x => x.Value),
            };
        }

        private void Log(string text)
        {
            textBlock.Text = text;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            Log(error.Message);
        }

        public void OnNext(Lot lot)
        {
            // TODO extract to base method
            var openPosition = ticks.IndexOf(lot.OpenTick);
            var closePosition = ticks.IndexOf(lot.CloseTick);
            var brush = lot is LongLot ? Brushes.Green : Brushes.Red;
            var pointGeometry = lot is LongLot
                ? Geometry.Parse("M 0 0 L 4 -4 L 8 0 Z")
                : Geometry.Parse("M 0 0 L 4 4 L 8 0 Z");
            var lotLine = new LineSeries
            {
                Values = new ChartValues<ObservablePoint>
                {
                    new ObservablePoint(openPosition, (double) lot.Open),
                    new ObservablePoint(closePosition, (double) lot.Close),
                },
                Fill = Brushes.Transparent,
                Stroke = brush,
                PointForeground = brush,
                PointGeometry = pointGeometry,
                PointGeometrySize = 10,
            };
            seriesCollection.Add(lotLine);
        }
    }
}
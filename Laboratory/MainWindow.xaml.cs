using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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

namespace Laboratory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DateTimePicker.SelectedDate = new DateTime(2020, 2, 20);
        }

        private async void RefreshChart(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDate = DateTimePicker.SelectedDate;
                if (!selectedDate.HasValue)
                    return;
            
                var historyProvider = new FinamQuotesHistoryProvider(
                    new FinamTicksParser(), 
                    new FinamCandlesParser(),
                    new FinamQuotesHistoryClient(
                        new HistoryConfiguration()));
            
                var candlesHistory = await historyProvider.GetCandlesHistory(
                    "GAZP",
                    TimeInterval.Minutes10,
                    selectedDate.Value.Date,
                    selectedDate.Value.Date.AddDays(1).AddSeconds(-1),
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
                LiveChart?.Refresh(candlesToShow, candlesDates, lots);
                LiveChart?.HideError();
                ReportBlock.Text = report.ToString();
            }
            catch (Exception exception)
            {
                LiveChart?.ShowError(exception.Message);
            }
        }
    }
}
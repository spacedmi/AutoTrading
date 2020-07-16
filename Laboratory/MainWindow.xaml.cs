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
                TimeInterval.Hours,
                selectedDate.Value,
                selectedDate.Value.AddDays(1),
                CancellationToken.None);

            var sandBoxRunner = new SandBoxRunner();
            var report = sandBoxRunner.RunStrategy(
                new List<Candle>(),
                new RandomStrategy(),
                candlesHistory.Candles.Select(x => new Tick(value: x.CloseValue, x.Volume, x.CloseDateTime)));

            var candles = new ChartValues<OhlcPoint>();
            candles.AddRange(candlesHistory.Candles.Select(x => 
                new OhlcPoint((double) x.OpenValue, (double) x.HighValue, (double) x.LowValue, (double) x.CloseValue)));

            var lots = new[]
            {
                new LineSeries
                {
                    Values = new ChartValues<double> {},
                    Fill = Brushes.Transparent,
                }
            };

            var candlesDates = candlesHistory.Candles.Select(x => x.CloseDateTime.ToString("hh:mm:ss"));
            LiveChart?.Refresh(candles, candlesDates, lots);
        }
    }
}
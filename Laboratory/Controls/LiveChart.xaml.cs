using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace AutoTrading.Laboratory.Controls
{
    public partial class LiveChart : UserControl, INotifyPropertyChanged
    {
        private string[] labels;
        private SeriesCollection seriesCollection;

        public LiveChart()
        {
            InitializeComponent();
            YFormatter = val => val.ToString("C");
            DataContext = this;
        }

        public Func<double, string> YFormatter { get; set; }

        public SeriesCollection SeriesCollection
        {
            get => seriesCollection;
            set
            {
                seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }

        public string[] Labels
        {
            get => labels;
            set
            {
                labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }
 
        public void Refresh(ChartValues<OhlcPoint> candles, IEnumerable<string> candlesDates, IEnumerable<LineSeries> lots)
        {
            SeriesCollection = new SeriesCollection
            {
                new CandleSeries
                {
                    Values = candles,
                },
            };
            SeriesCollection.AddRange(lots);
            Labels = candlesDates.ToArray();
            Y.MinValue = candles.Min(x => x.Close);
            Y.MaxValue = candles.Max(x => x.Close);
        }

        public void ShowError(string errorText)
        {
            ErrorBlock.Text = errorText;
            ErrorBlock.Visibility = Visibility.Visible;
        }
        
        public void HideError()
        {
            ErrorBlock.Visibility = Visibility.Hidden;
        }
 
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
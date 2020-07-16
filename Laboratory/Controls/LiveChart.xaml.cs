using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using LiveCharts;
using LiveCharts.Defaults;
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
            DataContext = this;
        }

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
                new OhlcSeries
                {
                    Values = candles,
                },
            };
            SeriesCollection.AddRange(lots);
            Labels = candlesDates.ToArray();
        }
 
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
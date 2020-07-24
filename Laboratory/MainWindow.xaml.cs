using System;
using System.Windows;
using AutoTrading.Laboratory.BackTesting;

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

                var visualizer = new RandomDailyCandlesVisualizer();
                var result = await visualizer.Visualize(selectedDate.Value);
                
                LiveChart.Refresh(result);
                LiveChart?.HideError();
                ReportBlock.Text = result.Report;
            }
            catch (Exception exception)
            {
                LiveChart?.ShowError(exception.Message);
            }
        }
    }
}
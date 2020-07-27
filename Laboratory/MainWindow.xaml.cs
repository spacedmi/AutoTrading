using System;
using System.Windows;
using System.Windows.Controls;
using AutoTrading.Laboratory.BackTesting;
using AutoTrading.Laboratory.BackTesting.Random;

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

                var visualizer = new RandomDailyTicksVisualizer(ReportBlock);
                var result = await visualizer.Visualize(selectedDate.Value);
                
                LiveChart.Refresh(result);
                LiveChart?.HideError();
            }
            catch (Exception exception)
            {
                LiveChart?.ShowError(exception.Message);
            }
        }
    }
}
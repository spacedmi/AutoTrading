using System;
using System.Threading.Tasks;

namespace AutoTrading.Laboratory.BackTesting
{
    public interface IDailyStrategyVisualizer
    {
        Task<LiveChartModel> Visualize(DateTime date);
    }
}
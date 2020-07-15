using System.Configuration;
using AutoTrading.QuotesHistory.Interfaces;

namespace AutoTrading.Application
{
    public class HistoryConfiguration : IHistoryConfiguration
    {
        public string FinamExportUrl => ConfigurationManager.AppSettings["FinamExportUrl"];
    }
}
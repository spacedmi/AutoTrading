using System.Configuration;
using AutoTrading.QuotesHistory.Interfaces;

namespace AutoTrading.Laboratory.Configuration
{
    public class HistoryConfiguration : IHistoryConfiguration
    {
        public string FinamExportUrl => ConfigurationManager.AppSettings["FinamExportUrl"];
    }
}
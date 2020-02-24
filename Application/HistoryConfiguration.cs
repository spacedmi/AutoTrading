using System.Configuration;
using QuotesHistory.Interfaces;

namespace TestApp
{
    public class HistoryConfiguration : IHistoryConfiguration
    {
        public string FinamExportUrl => ConfigurationManager.AppSettings["FinamExportUrl"];
    }
}
using System.Collections.Generic;

namespace AutoTrading.QuotesHistory.Models
{
    public class QuotesTicksHistoryModel
    {
        public ICollection<Tick> Ticks { get; set; }
    }
}
using System.Collections.Generic;

namespace QuotesHistory.Models
{
    public class QuotesTicksHistoryModel
    {
        public ICollection<Tick> Ticks { get; set; }
    }
}
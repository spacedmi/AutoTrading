using System.Collections.Generic;
using System.Linq;
using AutoTrading.Strategy.Models;

namespace AutoTrading.SandBox.Models
{
    public class Report
    {
        public Report(IEnumerable<Lot> lots)
        {
            Lots = lots.ToList();
        }

        public IReadOnlyCollection<Lot> Lots { get; }
        public decimal Profit => Lots.Where(x => x.Profit.HasValue).Sum(x => x.Profit.Value);
        public IReadOnlyCollection<Lot> ProfitableLots => Lots.Where(x => x.Profit.HasValue && x.Profit > 0).ToList();
        public IReadOnlyCollection<Lot> UnprofitableLots => Lots.Where(x => x.Profit.HasValue && x.Profit <= 0).ToList();
        public decimal ProfitableLotsSum => ProfitableLots.Sum(x => x.Profit.Value);
        public decimal UnprofitableLotsSum => UnprofitableLots.Sum(x => x.Profit.Value);
        
        public override string ToString()
        {
            return 
@$"Lots count: {Lots.Count}
{ProfitableLots.Count} profitable lots in amount {ProfitableLotsSum}
{UnprofitableLots.Count} unprofitable lots in amount  {UnprofitableLotsSum}
Profit: {Profit}";
        }
    }
}
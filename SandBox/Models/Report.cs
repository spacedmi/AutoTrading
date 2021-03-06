﻿using System.Collections.Generic;
using System.Linq;
using AutoTrading.Strategy;
using AutoTrading.Strategy.Models;

namespace AutoTrading.SandBox.Models
{
    public class Report
    {
        public Report(IStrategy strategy)
        {
            Lots = strategy.Lots.ToList();
        }

        public IReadOnlyCollection<Lot> Lots { get; }
        public IReadOnlyCollection<Lot> CloseLots =>  Lots.Where(x => x.Close.HasValue).ToList();
        public decimal Profit => Lots.Where(x => x.Profit.HasValue).Sum(x => x.Profit.Value);
        public IReadOnlyCollection<Lot> ProfitableLots => Lots.Where(x => x.Profit.HasValue && x.Profit > 0).ToList();
        public IReadOnlyCollection<Lot> UnprofitableLots => Lots.Where(x => x.Profit.HasValue && x.Profit <= 0).ToList();
        public decimal ProfitableLotsSum => ProfitableLots.Sum(x => x.Profit.Value);
        public decimal UnprofitableLotsSum => UnprofitableLots.Sum(x => x.Profit.Value);
        
        public override string ToString()
        {
            return 
@$"Lots count: {Lots.Count}
{ProfitableLots.Count} profitable lots: {ProfitableLotsSum}
{UnprofitableLots.Count} unprofitable lots:  {UnprofitableLotsSum}
Profit: {Profit}";
        }
    }
}
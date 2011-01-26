using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Model
    {
        private RepGenDataContext context;

        private RepGenDataContext DataContext
        {
            get
            {
                if (context == null)
                    context = new RepGenDataContext();
                return context;
            }
        }

        public static IQueryable<AssetWeighting> GetModelAllocation(string strategyId, bool hnw)
        {
            var ctx = new RepGenDataContext();

            var alloction = from model in ctx.Models
                            where model.StrategyID.Equals(strategyId)
                            group model by model.AssetClass.Name into g
                            let weight = hnw ? g.Sum(m => m.WeightingHNW) : g.Sum(m => m.WeightingAffluent)
                            select new AssetWeighting
                            {
                                AssetClass = g.Key,
                                Weighting = (double?)weight
                            };

            return alloction;
        }

        public static IQueryable<ModelAssetClass> GetAssetClasses(string strategyId)
        {
            var ctx = new RepGenDataContext();

            var classes = from m in ctx.Models
                          where m.StrategyID == strategyId
                          orderby m.AssetClass.Name
                          group m by m.AssetClassID
                              into g
                              select new ModelAssetClass
                              {
                                  ID = g.Key,
                                  Name = g.First().AssetClass.Name,
                              };
            return classes;
        }

        private static bool InModel(bool status, decimal weightingHNW, decimal weightingAffluent)
        {
            return status ? weightingHNW > 0 : weightingAffluent > 0;
        }

        public static decimal GetModelCost(string strategyId, bool status)
        {
            var ctx = new RepGenDataContext();

            var cost = from m in ctx.Models.Where(m => m.StrategyID == strategyId).ToList()
                       where InModel(status, m.WeightingHNW, m.WeightingAffluent)
                       group m by m.StrategyID
                           into g
                           select g.Sum(m => m.PurchaseCharge);

            return cost.SingleOrDefault();
        }
    }
}

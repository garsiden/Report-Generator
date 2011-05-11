using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class TacticalModel
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

        // All weighting info should be for Strategic Models
        //public static IQueryable<AssetWeighting> GetAssetWeighting(string strategyId, bool hnw)
        //{
        //    var ctx = new RepGenDataContext();

        //    var allocation = from model in ctx.TacticalModels
        //                    where model.StrategyID.Equals(strategyId)
        //                    group model by model.AssetGroup.Name into g
        //                    let weight = hnw ? g.Sum(m => m.WeightingHNW) : g.Sum(m => m.WeightingAffluent)
        //                    select new AssetWeighting {
        //                        AssetGroup = g.Key,
        //                        Weighting = (double?)weight
        //                    };

        //    return allocation;
        //}

        public static IQueryable<ModelAssetGroup> GetAssetGroups(string strategyId)
        {
            var ctx = new RepGenDataContext();

            var groups = from m in ctx.TacticalModels
                         where m.StrategyID == strategyId
                         orderby m.AssetGroup.Name
                         group m by m.AssetGroupID
                             into g
                             select new ModelAssetGroup {
                                 ID = g.Key,
                                 Name = g.First().AssetGroup.Name,
                             };
            return groups;
        }

        private static bool InModel(bool status, decimal weightingHNW, decimal weightingAffluent)
        {
            return status ? weightingHNW > 0 : weightingAffluent > 0;
        }

        public static decimal GetCost(string strategyId, bool status)
        {
            var ctx = new RepGenDataContext();

            var cost = from m in ctx.TacticalModels.Where(m => m.StrategyID == strategyId).ToList()
                       where InModel(status, m.WeightingHNW, m.WeightingAffluent)
                       group m by m.StrategyID
                           into g
                           select g.Sum(m => m.PurchaseCharge);

            return cost.SingleOrDefault();
        }
    }
}

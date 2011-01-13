using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class ModelBreakdown
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<ModelBreakdown> GetModelBreakdown(string strategyId, string assetGroupId)
        {
            var ctx = new RepGenDataContext();

            var breaks = from b in ctx.ModelBreakdowns
                         join g in ctx.AssetGroupClasses
                            on b.AssetClassID equals g.AssetClassID
                         where g.AssetGroupID == assetGroupId && b.StrategyID == strategyId
                         select b;

            return breaks;

        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
        public static void UpdateModelBreakdown(ModelBreakdown modelBreakdown, ModelBreakdown original_modelBreakdown)
        {
            var ctx = new RepGenDataContext();
            ctx.ModelBreakdowns.Attach(modelBreakdown, original_modelBreakdown);
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
        public static void UpdateModelBreakdown(ModelBreakdown modelBreakdown)
        {
            var ctx = new RepGenDataContext();
            ctx.ModelBreakdowns.Attach(modelBreakdown, true);
            ctx.SubmitChanges();
        }


    }
}

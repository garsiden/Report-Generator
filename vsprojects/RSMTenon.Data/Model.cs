using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    public partial class Model
    {
        private RepGenDataContext context;

        private RepGenDataContext getContext()
        {
            if (context == null)
                context = new RepGenDataContext();
            return context;
        }

        public static IQueryable<AssetWeighting> GetModelAllocation(string strategyId)
        {
            var context = new RepGenDataContext();
            var models = context.Models;

            var alloction = from model in models
                            where model.StrategyID.Equals(strategyId)
                            group model by model.AssetClass.Name into g
                            select new AssetWeighting { AssetClass = g.Key, Weighting = g.Sum(model => model.Weighting) };

            return alloction;
        }

    }
}

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
                    context = new RepGenDataContext(ConnectionFactory.CreateSqlConnection());
                return context;
            }
        }

        public static IQueryable<AssetWeighting> GetModelAllocation(string strategyId)
        {
            var ctx = new RepGenDataContext(ConnectionFactory.CreateSqlConnection());

            var alloction = from model in ctx.Models
                            where model.StrategyID.Equals(strategyId)
                            group model by model.AssetClass.Name into g
                            select new AssetWeighting
                            {
                                AssetClass = g.Key,
                                Weighting = g.Sum(model => model.Weighting)
                            };

            return alloction;
        }

        public static IQueryable<ModelAssetClass> GetAssetClasses(string strategyId)
        {
            var ctx = new RepGenDataContext(ConnectionFactory.CreateSqlConnection());

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
    }
}

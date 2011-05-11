using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class StrategicModel
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

        public static IQueryable<AssetWeighting> GetAssetWeighting(string strategyId)
        {
            var ctx = new RepGenDataContext();

            var weighting = from model in ctx.StrategicModels
                            where model.StrategyID.Equals(strategyId)
                            join classes in ctx.AssetClasses
                            on model.AssetClassID equals classes.ID
                            join groups in ctx.AssetGroups
                            on classes.AssetGroupID equals groups.ID
                            group model by groups.Name
                                into g
                                select new AssetWeighting {
                                    AssetGroup = g.Key,
                                    Weighting = (double?)g.Sum(m => m.Weighting)
                                };

            return weighting;
        }

        public static IQueryable<ModelAssetClass> GetNewAssetClasses(string strategyId)
        {
            var ctx = new RepGenDataContext();

            var strategyModel = from m in ctx.StrategicModels
                                where m.StrategyID == strategyId
                                select m;

            var newClasses = from _class in ctx.AssetClasses
                             join model in strategyModel
                             on _class.ID equals model.AssetClassID
                             into allClasses
                             from model in allClasses.DefaultIfEmpty()
                             where model == null
                             select new ModelAssetClass {
                                 ID = _class.ID,
                                 Name = _class.Name
                             };

            return newClasses;

        }

        //public static IQueryable<ModelAssetGroup> GetAssetGroups(string strategyId)
        //{
        //    var ctx = new RepGenDataContext();

        //    var classes = from m in ctx.StrategicModels
        //                  where m.StrategyID == strategyId
        //                  //orderby m.AssetGroup.Name
        //                  group m by m.AssetGroupID
        //                      into g
        //                      select new ModelAssetGroup {
        //                          ID = g.Key,
        //                          Name = g.First().AssetGroup.Name,
        //                      };
        //    return classes;
        //}

    }
}

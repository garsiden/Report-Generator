using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Content
    {
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Content> GetContent(string strategyId, int contentIdx, int categoryIdx)
        {
            var ctx = new RepGenDataContext();
            var predicate = PredicateBuilder.Make<RSMTenon.Data.Content>();

            switch (contentIdx)
            {
                case 0:
                    predicate = predicate.And(c => c.ContentID.StartsWith("strategy."));
                    break;
                case 1:
                    predicate = predicate.And(c => c.ContentID.StartsWith("charts."));
                    break;
                case 2:
                    predicate = predicate.Or(c => c.ContentID.Length > 0);
                    break;
            }

            switch (categoryIdx)
            {
                case 0:
                    predicate = predicate.And(c => c.Category == "existing-assets");
                    break;
                case 1:
                    predicate = predicate.And(c => c.Category == "no-existing-assets");
                    break;
            }

            switch (strategyId)
            {
                case "%":
                    //predicate = predicate.And(c => c.StrategyID != null);
                    break;
                case "":
                    predicate = predicate.And(c => c.StrategyID == null);
                    break;                  
                default:
                    predicate = predicate.And(c => c.StrategyID == strategyId);
                    break;
            }

            var contents = ctx.Contents;

            var match = contents.Where(predicate).OrderBy(c => c.ContentID).ThenBy(c => c.Category);

            return match;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Content> GetContent(string strategyId, bool strategy, bool charts, bool cash, bool assets, bool general)
        {
            var ctx = new RepGenDataContext();

            var predicate = PredicateBuilder.Make<RSMTenon.Data.Content>();


            if (!charts)
                predicate = predicate.And(c => !c.ContentID.StartsWith("charts."));
            else
                predicate = predicate.Or(c => c.ContentID.StartsWith("charts."));

            //if (!cash)
            //    predicate = predicate.Or(c => !c.Category.Equals("no-existing-assets"));
            //if (!assets)
            //    predicate = predicate.Or(c => !c.Category.Equals("existing-assets"));
            //if (!general)
            //    predicate = predicate.Or(c => c.StrategyID != null);
            if (strategy)
                switch (strategyId)
                {
                    case "%":
                        predicate = predicate.And(c => c.StrategyID != null);
                        break;
                    default:
                        predicate = predicate.And(c => c.StrategyID == strategyId);
                        break;
                } else
                predicate = predicate.And(c => c.StrategyID == null);



            var contents = ctx.Contents;

            var match = contents.Where(predicate).OrderBy(c => c.ContentID).ThenBy(c => c.Category);

            return match;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Content> GetContent(string strategyId)
        {
            var ctx = new RepGenDataContext();

            var predicate = PredicateBuilder.Make<RSMTenon.Data.Content>();

            switch (strategyId)
            {
                case "":
                    predicate = PredicateBuilder.Make<RSMTenon.Data.Content>(c => c.StrategyID == null);
                    break;
                case "%":
                    predicate = PredicateBuilder.Make<RSMTenon.Data.Content>(c => c.StrategyID != null);
                    break;
                default:
                    predicate = PredicateBuilder.Make<RSMTenon.Data.Content>(c => c.StrategyID == strategyId);
                    break;
            }

            //var predicate = PredicateBuilder.Make<Customer>(c => c.Name.Contains("A"));
            //predicate = predicate.Or(c => c.Name.Contains("B"));
            //predicate = predicate.Or(c => c.Name.Contains("C"));
            //predicate = predicate.Or(c => c.Name.Contains("X") && c.Age > 30);
            //predicate = predicate.Or(c => c.Age > 50 && c.Age < 55);

            var contents = ctx.Contents;

            var match = contents.Where(predicate).OrderBy(c => c.ContentID).ThenBy(c => c.Category);

            return match;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
        public static void UpdateClient(Content content, Content original_content)
        {
            var ctx = new RepGenDataContext();
            ctx.Contents.Attach(content, original_content);
            ctx.SubmitChanges();
        }


    }
}

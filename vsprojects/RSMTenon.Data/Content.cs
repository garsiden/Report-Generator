using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Content
    {
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static IQueryable<Content> GetContents(string strategyId, int categoryIdx, string contentId)
        {
            var ctx = new RepGenDataContext();
            var predicate = PredicateBuilder.Make<RSMTenon.Data.Content>();

            if (contentId != "All")
            {
                predicate = predicate.And(c => c.ContentID == contentId);
            } else
            {
                predicate = predicate.And(c => c.ContentID != null);
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
                case "Any":
                    predicate = predicate.And(c => c.StrategyID != null);
                    break;
                case "None":
                    predicate = predicate.And(c => c.StrategyID == null);
                    break;
                case "AnyOrNone":
                    break;
                default:
                    predicate = predicate.And(c => c.StrategyID == strategyId);
                    break;
            }

            var contents = ctx.Contents;
            var match = contents.Where(predicate).OrderBy(c => c.ContentID).ThenBy(c => c.Category).ThenBy(c => c.StrategyID);

            return match;
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Content> GetContents(string strategyId)
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
        public static void UpdateContent(Content content, Content original_content)
        {
            var ctx = new RepGenDataContext();
            ctx.Contents.Attach(content, original_content);
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Content> GetContents(string strategyId, string category)
        {
            var ctx = new RepGenDataContext();

            var match = from c in ctx.Contents
                        where (c.StrategyID.Equals(strategyId) || c.StrategyID.Equals(null)) &&
                          (c.Category.Equals(category) || c.Category.Equals(null))
                        select c;
            return match;
        }
    }
}

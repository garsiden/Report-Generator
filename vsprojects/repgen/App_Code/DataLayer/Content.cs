using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Content
    {
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Content> GetContent(string strategyId)
        {
            var ctx = new RepGenDataContext(ConnectionFactory.CreateSqlConnection());

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

            var match = contents.Where(predicate);

            return match;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
        public static void UpdateClient(Content content, Content original_content)
        {
            var ctx = new RepGenDataContext(ConnectionFactory.CreateSqlConnection());
            ctx.Contents.Attach(content, original_content);
            ctx.SubmitChanges();
        }


    }
}

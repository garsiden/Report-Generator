using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    public partial class Strategy
    {
        private Dictionary<int, ReturnData> strategyReturn;

        public static string GetStrategyNameFromId(string id)
        {
            var ctx = new RepGenDataContext();

            return ctx.Strategies.First(s => s.ID.Equals(id)).Name;

        }

        public Dictionary<int, ReturnData> GetStrategyReturn()
        {
            if (strategyReturn == null) {
                var ctx = new RepGenDataContext();
                var returns = ctx.ModelReturn(this.ID);
                var calc = new ReturnCalculation();
                var prices = from p in returns
                             select new ReturnData {
                                 Date = p.Date,
                                 Value = calc.Price(p)
                             };

                strategyReturn = prices.ToDictionary(p => p.Date);
            }

            return strategyReturn;
        }
    }
}

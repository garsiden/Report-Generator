using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    public partial class Strategy
    {
        public static string GetStrategyNameFromId(string id)
        {
            var ctx = new RepGenDataContext();

            return ctx.Strategies.First(s => s.ID.Equals(id)).Name;

        } 
    }
}

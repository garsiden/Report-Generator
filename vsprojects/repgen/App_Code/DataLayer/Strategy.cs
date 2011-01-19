﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Strategy
    {
        private Dictionary<int, ReturnData> strategyReturn;

        public static string GetStrategyNameFromId(string id)
        {
            var ctx = new RepGenDataContext();

            return ctx.Strategies.First(s => s.ID.Equals(id)).Name;
        }

        public Dictionary<int, ReturnData> GetStrategyReturn(string status)
        {
            if (strategyReturn == null)
            {
                var ctx = new RepGenDataContext();
                var returns = ctx.ModelReturn(this.ID, status);
                var calc = new ReturnCalculation();
                var prices = from p in returns
                             select new ReturnData
                             {
                                 Date = p.Date,
                                 Value = calc.Price(p)
                             };

                strategyReturn = prices.ToDictionary(p => p.Date);
            }

            return strategyReturn;
        }

        public static List<Strategy> GetStrategies()
        {
            var ctx = new RepGenDataContext();
            return ctx.Strategies.ToList();
        }
    }
}

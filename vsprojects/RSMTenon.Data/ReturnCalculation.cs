using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    public class ReturnCalculation
    {
        private double previousPrice = 100D;
        private double previousDD = 1D;
        private double previousPriceRR = 100D;
        private double previousPriceRebase = 100D;

        public double ModelPrice(ReturnData rtrn)
        {
            double price = (1 + rtrn.Value) * previousPrice;
            previousPrice = price;

            return price;
        }

        public double IndexPrice(ReturnData rtrn)
        {
            double price = previousPrice * Math.Exp(rtrn.Value);
            previousPrice = price;

            return price;
        }

        public double IndexReturn(ReturnData price)
        {
            double rtrn = Math.Log(price.Value / previousPrice);
            previousPrice = price.Value;

            return rtrn;
        }

        public double ModelReturn(ReturnData price)
        {
            double retval = (price.Value - previousPrice) / previousPrice;
            previousPrice = price.Value;

            return retval;
        }

        public double RollingReturn(ReturnData price)
        {
            double rr = Math.Log(price.Value / previousPriceRR);
            previousPriceRR = price.Value;

            return rr;
        }

        public double RollingReturn(ReturnData price, ReturnData prevPrice)
        {
            return RollingReturn(price, prevPrice, 1);
        }

        public double RollingReturn(ReturnData price, ReturnData prevPrice, int years)
        {
            double rr = Math.Log(price.Value / prevPrice.Value) / years;

            return rr;
        }

        public double Drawdown(double price, double rtrn)
        {
            double dd = 0D;

            if ((price / previousPrice > 1) && previousDD == 1) {
                dd = 1D;
            } else {
                dd = Math.Min(1D, previousDD * (1 + rtrn));
                this.previousDD = dd;
            }

            this.previousPrice = price;

            return dd;
        }

        public double Drawdown(double price, ReturnData rtrn)
        {
            return Drawdown(price, rtrn.Value);
        }

        public double RebaseReturn(ReturnData rtrn)
        {
            return RebaseReturn(rtrn.Value);
        }

        public double RebaseReturn(double rtrn)
        {
            double rebase = previousPriceRebase * Math.Exp(rtrn);
            previousPriceRebase = rebase;

            return (rebase - 100) / 100;
        }
    }
}

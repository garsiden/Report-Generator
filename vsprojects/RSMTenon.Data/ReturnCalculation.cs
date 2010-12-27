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
        private double previousRebase = 100D;
        //private double previousReturn = 100D; // 101.4504179D;


        public double Price(ReturnData returnData)
        {
            double retval = (1 + returnData.Value) * previousPrice;
            previousPrice = retval;

            return retval;
        }

        public double RollingReturn(ReturnData returnData)
        {
            double retval = Math.Log(returnData.Value / previousPriceRR);
            previousPriceRR = returnData.Value;

            return retval;
        }

        public double RollingReturn(ReturnData returnData1, ReturnData returnData2)
        {
            return RollingReturn(returnData1, returnData2, 1);
        }

        public double RollingReturn(ReturnData returnData1, ReturnData returnData2, int years)
        {
            double retval = Math.Log(returnData1.Value / returnData2.Value)/ years;

            return retval;
        }

        public double Drawdown(double price)
        {
            double retval = 0D;

            if ((price / previousPrice > 1) && previousDD == 1) {
                retval = 1D;
            } else {
                retval = Math.Min(1D, previousDD * (1 + Math.Log(price / previousPrice)));
                this.previousDD = retval;
            }

            this.previousPrice = price;

            return retval;
        }

        public double Drawdown(double price, double rtrn)
        {
            double retval = 0D;

            if ((price / previousPrice > 1) && previousDD == 1) {
                retval = 1D;
            } else {
                retval = Math.Min(1D, previousDD * (1 + rtrn));
                this.previousDD = retval;
            }

            this.previousPrice = price;

            return retval;
        }

        public double Drawdown(ReturnData price)
        {
            return Drawdown(price.Value);
        }

        public double RebaseReturn(ReturnData rtrn)
        {
            return RebaseReturn(rtrn.Value);
        }

        public double RebaseReturn(double rtrn)
        {
            double rebase = previousRebase * Math.Exp(rtrn);
            previousRebase = rebase;

            return (rebase - 100) / 100;
        }

        public double Return(ReturnData price)
        {
            double retval = (price.Value - previousPrice) / previousPrice;
            previousPrice = price.Value;

            return retval;
        }

    }
}

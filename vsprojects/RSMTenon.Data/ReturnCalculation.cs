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

        public double calculatePrice(ReturnData returnData)
        {
            double retval = (1 + returnData.Value) * previousPrice;
            previousPrice = retval;
            return retval;
        }

        public double calculateRollingReturn(ReturnData returnData)
        {
            double retval = Math.Log(returnData.Value / previousPriceRR);
            previousPriceRR = returnData.Value;
            return retval;
        }

        public double calculateRollingReturn(ReturnData returnData1, ReturnData returnData2)
        {
            return calculateRollingReturn(returnData1, returnData2, 1);
        }

        public double calculateRollingReturn(ReturnData returnData1, ReturnData returnData2, int years)
        {
            double retval = Math.Log(returnData1.Value / returnData2.Value)/ years;
            //previousPriceRR = returnData.Value;
            return retval;
        }


        public double calculateDrawdown(DrawdownData drawdown)
        {
            if ((drawdown.Value / drawdown.PreviousValue > 1) && previousDD == 1) {
                return 1D;
            } else {
                double retval = Math.Min(1D, previousDD * (1 + Math.Log(drawdown.Value / drawdown.PreviousValue)));
                this.previousDD = retval;
                return retval;
            }
        }

        public double rebaseReturn(ReturnData rd)
        {
            double rebase = previousRebase * Math.Exp(rd.Value);
            previousRebase = rebase;

            return (rebase - 100) / 100;
        }

    }
}

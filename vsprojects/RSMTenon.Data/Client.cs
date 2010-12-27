using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    public partial class Client
    {

        //private Strategy strategy;

        public static Client GetClientByGUID(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();

            return ctx.Clients.Single(c => c.GUID == clientGuid);
        }

        public string ReportingFrequency
        {
            get
            {
                if (this.InvestmentAmount >= 1000000) {
                    return "quarterly";
                } else {
                    return "half-yearly";
                }
            }
        }

        public decimal InitialFeeAmount
        {
            get
            {
                return this.InitialFee * this.InvestmentAmount;
            }
        }
    }
}

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

        public static List<Client> GetRecentClients(int number)
        {
            var ctx = new RepGenDataContext();

            return ctx.Clients.OrderByDescending(c => c.MeetingDate).Take(number).ToList();

            //var clients = ctx.Clients;

            //var recent = from c in clients
            //             orderby c.MeetingDate descending
            //             select new
            //             {
            //                 GUID = c.GUID,
            //                 Name = c.Name,
            //                 MeetingDate = c.MeetingDate
            //             };

            //return recent.Take(number).ToList();
                            
        }
    }
}

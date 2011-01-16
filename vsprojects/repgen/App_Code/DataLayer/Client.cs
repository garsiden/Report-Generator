using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Client
    {
        #region DataObjectMethods
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static Client GetClientByGUID(Guid? clientGuid)
        {

            if (clientGuid == null)
            {
                return null;
            } else
            {
                var ctx = new RepGenDataContext();
                return ctx.Clients.SingleOrDefault(c => c.GUID == clientGuid);
            }
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Client> GetRecentClients(int number)
        {
            if (number > 0)
            {
                var ctx = new RepGenDataContext();
                return ctx.Clients.OrderByDescending(c => c.MeetingDate).Take(number);
            } else
            {
                return null;
            }
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateClient(Client client)
        {
            var ctx = new RepGenDataContext();
            ctx.Clients.Attach(client, true);
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
        public static void UpdateClient(Client client, Client original_client)
        {
            var ctx = new RepGenDataContext();
            ctx.Clients.Attach(client, original_client);
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public static Guid InsertClient(Client client)
        {
            var ctx = new RepGenDataContext();
            client.UserID = ReportGenerator.ReportGenerator.GetUserId();
            ctx.Clients.InsertOnSubmit(client);
            ctx.SubmitChanges();

            return client.GUID;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
        public static void DeleteClient(Guid guid)
        {
            var ctx = new RepGenDataContext();
            ctx.Clients.DeleteOnSubmit(ctx.Clients.Single(c => c.GUID == guid));
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public static void DeleteClient(Client client)
        {
            var ctx = new RepGenDataContext();
            ctx.Clients.Attach(client);
            ctx.Clients.DeleteOnSubmit(client);
            ctx.SubmitChanges();
        }

        #endregion DataObjectMethods

        #region Extended Properties
        public string ReportingFrequency
        {
            get
            {
                if (this.InvestmentAmount >= 1000000)
                {
                    return "quarterly";
                } else
                {
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

        public bool Affluent
        {
            get
            {
                return !HighNetWorth;
            }
            set
            {
                HighNetWorth = !value;
            }
        }

        public int Status
        {
            get
            {
                return HighNetWorth ? 0 : 1;
            }
            set
            {
                HighNetWorth = (value == 0);
            }
        }

        public string StatusName
        {
            get
            {
                return HighNetWorth ? "HNW" : "AFF";
            }
        }

        #endregion

        #region Data Validation
        partial void OnInitialFeeChanging(decimal value)
        {
            if ((value < 0 || value > 5))
            {
                throw new ArgumentException("Initial Fee Percent must be between 0 and 5");
            }
        }

        partial void OnInvestmentAmountChanging(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Investment Amount must be greater than 0");
            }
        }

        partial void OnMeetingDateChanging(System.DateTime value)
        {
            if (value != null && value > DateTime.Today)
            {
                throw new ArgumentException("Meeting Date must be less than or equal to today");
            }
        }

        #endregion Data Validation
    }
}

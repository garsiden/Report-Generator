using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Client : Data.RepGenDataContext
    {
        #region DataObjectMethods
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static Client GetClientByGUID(Guid? clientGuid)
        {
            var ctx = new RepGenDataContext();
            if (clientGuid == null) {
                return null;
            } else {
                return ctx.Clients.SingleOrDefault(c => c.GUID == clientGuid);
            }
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static List<Client> GetRecentClients(int number)
        {
            var ctx = new RepGenDataContext();
            return ctx.Clients.OrderByDescending(c => c.MeetingDate).Take(number).ToList();
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

        #endregion
    }
}

﻿using System;
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

            if (clientGuid == null) {
                return null;
            } else {
                var ctx = new RepGenDataContext();
                return ctx.Clients.SingleOrDefault(c => c.GUID == clientGuid);
            }
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Client> GetRecentClients(int number)
        {
            if (number > 0) {
                var ctx = new RepGenDataContext();
                return ctx.Clients.OrderByDescending(c => c.DateIssued).Take(number);
            } else {
                return null;
            }
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<Client> GetRecentClients(int number, string userId)
        {
            if (number > 0) {
                var ctx = new RepGenDataContext();
                return ctx.Clients.Where(c => c.UserID == userId).OrderByDescending(c => c.DateIssued).Take(number);
            } else {
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

        public bool HasAssetsByClass
        {
            get
            {
                return this.ClientAssetClass != null;
            }
        }

        public bool HasAssetsByInvestment
        {
            get
            {
                return this.ClientAssets.Count > 0;
            }
        }

        public void ValidateClientAssets()
        {
            if (!ExistingAssets)
                return;

            // check for client investments first
            var assets = this.ClientAssets.ToList();

            if (assets != null && assets.Count > 0) {
                // check investment amount
                if (assets.Sum(a => a.Amount) != InvestmentAmount)
                    throw new Exception("Client assets do not equal the Investment Amount");
                // check individual allocations
                foreach (var item in assets) {
                    if (item.TotalAssetAllocation != 100) {
                        string msg = String.Format("Allocations of client investment {0} to asset classes do not total 100%", item.AssetName);
                        throw new Exception(msg);
                    }
                }
                return;
            }
            
           // Or check assets by class 
            if (ClientAssetClass == null)
                throw new Exception(@"No client assets entered.<br/>Please add client's assets or uncheck 'Use Existing Assets'.");

            if (ClientAssetClass.TotalAssetAllocation != 100)
                throw new Exception("Client assets by class do not total 100%.");
        }

        #endregion

        #region Data Validation

        partial void OnInvestmentAmountChanging(decimal value)
        {
            if (value <= 0) {
                throw new ArgumentException("Investment Amount must be greater than 0");
            }
        }

        #endregion Data Validation

        #region Events

        partial void OnStrategyIDChanging(string value)
        {
            if (value == "TC")
                this.HighNetWorth = true;
        }
        #endregion
    }
}

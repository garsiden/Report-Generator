using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class ClientAssetClass
    {
        #region DataObjectMethods
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static ClientAssetClass GetClientAssetClass(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();
            return ctx.ClientAssetClasses.SingleOrDefault(a => a.ClientGUID == clientGuid);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static IQueryable<ClientAssetClass> GetAllClientsAssets()
        {
            var ctx = new RepGenDataContext();
            return ctx.ClientAssetClasses;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
        public static void UpdateClientAsset(ClientAssetClass clientAsset, ClientAssetClass original_clientAsset)
        {
            var ctx = new RepGenDataContext();
            ctx.ClientAssetClasses.Attach(clientAsset, original_clientAsset);
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public static void DeleteClientAsset(ClientAssetClass clientAsset)
        {
            var ctx = new RepGenDataContext();
            ctx.ClientAssetClasses.DeleteOnSubmit(ctx.ClientAssetClasses.Single(c => c.ClientGUID == clientAsset.ClientGUID));
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
        public static void DeleteClientAsset(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();
            ctx.ClientAssetClasses.DeleteOnSubmit(ctx.ClientAssetClasses.Single(c => c.ClientGUID == clientGuid));
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertClientAsset(ClientAssetClass clientAsset)
        {
            var ctx = new RepGenDataContext();
            ctx.ClientAssetClasses.InsertOnSubmit(clientAsset);
            ctx.SubmitChanges();
        }

        #endregion

        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            if (action == ChangeAction.Insert || action == ChangeAction.Update) {
                if (TotalAssetAllocation != 100) {
                    string msg = String.Format("Asset allocations must total 100% (currently {0:0.0}%)", TotalAssetAllocation);
                    throw new ArgumentException(msg);
                }
            }
        }

        #region Extended Properties
        public decimal TotalAssetAllocation
        {
            get
            {
                return rnd(CASH) + rnd(COMM) + rnd(COPR) + rnd(GLEQ) + rnd(HEDG) + rnd(LOSH) + rnd(PREQ) + rnd(UKCB) + rnd(UKEQ) + rnd(UKGB) + rnd(UKHY) + rnd(WOBO);
            }
        }
        #endregion

        public static List<AssetWeighting> GetClientAssetWeighting(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();
            return ctx.ClientAssetWeighting(clientGuid).ToList();
        }

        private decimal rnd(decimal d) { return Math.Round(d, 1); }
    }
}

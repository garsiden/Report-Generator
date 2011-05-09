using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class ClientAsset
    {
        #region DataObjectMethods
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static ClientAsset GetClientAsset(Guid? guid)
        {
            if (guid != null)
            {
                var ctx = new RepGenDataContext();
                return ctx.ClientAssets.SingleOrDefault(a => a.GUID == guid);
            }

            return null;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static IQueryable<ClientAsset> GetAllClientsAssets(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();
            var clients = ctx.ClientAssets.Where(a => a.ClientGUID == clientGuid);
            return clients;

        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
        public static void UpdateClientAsset(ClientAsset clientAsset, ClientAsset original_clientAsset)
        {
            var ctx = new RepGenDataContext();
            ctx.ClientAssets.Attach(clientAsset, original_clientAsset);
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public static void DeleteClientAsset(ClientAsset clientAsset)
        {
            var ctx = new RepGenDataContext();
            ctx.ClientAssets.Attach(clientAsset);
            ctx.ClientAssets.DeleteOnSubmit(clientAsset);
            ctx.SubmitChanges();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public static Guid InsertClientAsset(ClientAsset clientAsset)
        {
            var ctx = new RepGenDataContext();
            ctx.ClientAssets.InsertOnSubmit(clientAsset);
            ctx.SubmitChanges();

            return clientAsset.GUID;
        }

        #endregion

        #region Extended Properties
        public decimal TotalAssetAllocation
        {
            get
            {
                return CASH + COMM + COPR + GLEQ + HEDG + LOSH + PREQ + UKCB + UKEQ + UKGB + UKHY + WOBO;
            }
        }
        #endregion

        #region Data Validation

        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            if (action == ChangeAction.Update)
            {
                if (TotalAssetAllocation != 100 && ClientGUID != Guid.Empty)
                {
                    string msg = String.Format("Asset allocations must total 100% (currently {0:0.0}%)", TotalAssetAllocation);
                    throw new ArgumentException(msg);
                }
            }

        }

        partial void OnAmountChanging(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Investment Amount must be greater than 0");
            }
        }

        #endregion
    }
}
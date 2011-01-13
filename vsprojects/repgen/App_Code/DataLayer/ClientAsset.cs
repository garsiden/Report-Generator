using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class ClientAsset
    {
        #region DataObjectMethods
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static ClientAsset GetClientAsset(Guid guid)
        {
            var ctx = new RepGenDataContext();
            return ctx.ClientAssets.SingleOrDefault(a => a.GUID == guid);

        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static IQueryable<ClientAsset> GetAllClientsAssets(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();
            return ctx.ClientAssets.Where(a => a.ClientGUID == clientGuid);

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
            ctx.ClientAssets.DeleteOnSubmit(ctx.ClientAssets.Single(c => c.GUID == clientAsset.GUID));
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
        public decimal TotalAssetAllocation
        {
            get
            {
                return CASH + COMM + COPR + GLEQ + HEDG + + LOSH + PREQ + UKCB + UKEQ + UKGB + UKHY + WOBO;
            }
        }

    }
}
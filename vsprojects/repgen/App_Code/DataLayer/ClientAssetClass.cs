using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        //public static IQueryable<AssetWeighting> GetClientAssetClass(Guid clientGuid)
        //{
        //    var ctx = new RepGenDataContext();
        //    var assets = ctx.ClientAssetClasses;

        //    var weighting = from asset in assets
        //                    where asset.ClientGUID.Equals(clientGuid)
        //                    //group asset by asset.AssetClass.Name  into g
        //                    //select new AssetWeighting { AssetClass = g.Key, Allocation = g.Sum(asset => asset.Weighting) };
        //                    select new AssetWeighting { AssetClass = asset.AssetClass.Name, Weighting = asset.Weighting };
        //    return weighting;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{

    public partial class ClientAssetClass
    {

        public static IQueryable<AssetWeighting> GetClientAssetClass(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();
            var assets = ctx.ClientAssetClasses;

            var weighting = from asset in assets
                            where asset.ClientGUID.Equals(clientGuid)
                            //group asset by asset.AssetClass.Name  into g
                            //select new AssetWeighting { AssetClass = g.Key, Allocation = g.Sum(asset => asset.Weighting) };
                            select new AssetWeighting { AssetClass = asset.AssetClass.Name, Weighting = asset.Weighting };
            return weighting;

        }
    }
}

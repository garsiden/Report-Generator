using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class AssetGroup
    {
        public static string GetAssetGroupNameFromId(string id)
        {
            var ctx = new RepGenDataContext();
            var asset = ctx.AssetGroups.SingleOrDefault(a => a.ID == id);
            if (asset != null)
                return asset.Name;
            else
                return null;
        }

        public static IQueryable<AssetGroup> GetAssetGroups()
        {
            var ctx = new RepGenDataContext();
            return ctx.AssetGroups;
        }
    }
}
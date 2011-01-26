using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class AssetClass
    {
        public string ColourHex { get; set; }

        public static string GetAssetClassNameFromId(string id)
        {
            var ctx = new RepGenDataContext();
            var asset = ctx.AssetClasses.SingleOrDefault(a => a.ID == id);
            if (asset != null)
                return asset.Name;
            else
                return null;
        }
    }
}

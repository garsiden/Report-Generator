using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    // Model allocation weightings
    public class AssetWeighting
    {
        public string AssetClass { get; set; }
        public decimal Weighting { get; set; }
    }

    public class RankedReturnData : ReturnData
    {
        public int RankNumber;
    }

    public class ModelTableData
    {
        public string AssetClassId;
        public string AssetClassName;
        public IGrouping<string, Model> Investments;
        public decimal Weighting;
    } 
}

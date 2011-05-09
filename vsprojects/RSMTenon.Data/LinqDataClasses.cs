using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace RSMTenon.Data
{
    // Model allocation weightings
    //public class AssetWeighting
    //{
    //    public string AssetClass { get; set; }
    //    public decimal Weighting { get; set; }
    //}

    public class RankedReturnData : ReturnData
    {
        public int RankNumber;
    }

    public class ModelTableData
    {
        public string AssetClassId;
        public string AssetClassName;
        public IGrouping<string, TacticalModel> Investments;
        public decimal Weighting;
    }

    public class ModelAssetClass
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}

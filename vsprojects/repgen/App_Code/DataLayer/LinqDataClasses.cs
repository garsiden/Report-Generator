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
        public IGrouping<string, Model> Investments;
        public decimal Weighting;
    }

    public class ModelAssetClass
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public partial class RepGenDataContext : System.Data.Linq.DataContext
    {
        [Function(Name = "dbo.spClientAssetReturn")]
        public ISingleResult<ReturnData> ClientAssetReturn([Parameter(Name = "ClientGUID", DbType = "UniqueIdentifier")] System.Nullable<System.Guid> clientGUID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), clientGUID);
            return ((ISingleResult<ReturnData>)(result.ReturnValue));
        }
        [Function(Name = "dbo.spModelReturn")]
        public ISingleResult<ReturnData> ModelReturn([Parameter(DbType = "Char(2)")] string strategyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), strategyId);
            return ((ISingleResult<ReturnData>)(result.ReturnValue));
        }
    }
}

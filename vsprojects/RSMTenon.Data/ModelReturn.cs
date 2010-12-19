using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace RSMTenon.Data
{
    public partial class RepGenDataContext : System.Data.Linq.DataContext

    {
        [Function(Name = "dbo.spModelReturn")]
        public ISingleResult<ReturnData> ModelReturn([Parameter(DbType = "Char(2)")] string strategyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), strategyId);
            return ((ISingleResult<ReturnData>)(result.ReturnValue));
        }


    }
}

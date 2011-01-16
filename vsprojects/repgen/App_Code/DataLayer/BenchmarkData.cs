using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSMTenon.Data
{
    public partial class BenchmarkData
    {
        #region Data Validation


        partial void OnDateChanging(System.DateTime value)
        {
            if (value < new DateTime(1997,1,1) || value > DateTime.Today)
            {
                throw new ArgumentException("Date must be less than or equal to today");
            }
        }

        #endregion Data Validation

    }
}

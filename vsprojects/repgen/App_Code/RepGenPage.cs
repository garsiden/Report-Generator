using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class RepGenPage : System.Web.UI.Page

{
    protected RepGenDataContext context;

    public IEnumerable<Strategy> GetStrategies()
    {
        return DataContext.Strategies;
    }

    public IEnumerable<Benchmark> GetBenchmarks()
    {
        return DataContext.Benchmarks.OrderBy(b => b.Name);
    }

    protected RepGenDataContext DataContext
    {
        get
        {
            if (context == null)
            {
                context = new RepGenDataContext();
            }
            return context;
        }
    }

}

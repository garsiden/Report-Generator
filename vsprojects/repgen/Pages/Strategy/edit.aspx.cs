using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Strategy_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public IEnumerable<Benchmark> GetBenchmarks()
    {
        var ctx = new RepGenDataContext();

        return ctx.Benchmarks.OrderBy(b => b.Name);
    }

}

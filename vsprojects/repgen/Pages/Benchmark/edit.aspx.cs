using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Benchmark_edit : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gridBenchmarkData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {

            string[] fields = { "Date", "ACMA", "BAMA", "CAMA", "GLGR", "STBO" };
            ListDictionary listDictionary = CreateListDictionaryFromGridFooter(fields, gridBenchmarkData);
            sourceBenchmarkData.Insert(listDictionary);
            gridBenchmarkData.DataBind();
        }

    }
}

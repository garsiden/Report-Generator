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
        labelException.Visible = false;
    }

    protected void gridBenchmarkData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try {
            if (e.CommandName == "Insert") {

                string[] fields = { "Date", "ACMA", "BAMA", "CAMA", "GLGR", "STBO" };
                ListDictionary listDictionary = CreateListDictionaryFromGridFooter(fields, gridBenchmarkData);
                sourceBenchmarkData.Insert(listDictionary);
                gridBenchmarkData.DataBind();
            }
        } catch (Exception ex) {
            showException(ex, labelException, "adding the benchmark prices");
        }
    }

    protected void gridBenchmarkData_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "deleting the benchmark prices");
            e.ExceptionHandled = true;
        }
    }

    protected void gridBenchmarkData_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "updating the benchmark prices");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }
}

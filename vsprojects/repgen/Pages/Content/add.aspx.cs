using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Content_add : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           formView.ChangeMode(FormViewMode.Insert);
        }

        labelException.Visible = false;
    }

    protected void formView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        if (e.Values["StrategyID"].ToString() == "None")
            e.Values["Strategy"] = null;
        if (e.Values["Category"].ToString() == "None")
            e.Values["Category"] = null;
    }

    protected void formView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            showException(e.Exception, labelException, "inserting the client");
            e.ExceptionHandled = true;
            e.KeepInInsertMode = true;
        }
    }
}

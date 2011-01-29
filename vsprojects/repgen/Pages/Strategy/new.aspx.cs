using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Strategy_new : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            detailsView.ChangeMode(DetailsViewMode.Insert);
        }

        labelException.Visible = false;
    }

    protected void detailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "adding the strategy");
            e.ExceptionHandled = true;
            e.KeepInInsertMode = true;
        }
    }
}

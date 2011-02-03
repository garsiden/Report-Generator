using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Strategy_index : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labelException.Visible = false;
    }

    protected void gridStrategy_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "updating the strategy");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }

    protected void gridStrategy_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "deleting the strategy");
            e.ExceptionHandled = true;
        }
    }
}

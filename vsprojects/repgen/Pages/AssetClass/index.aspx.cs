using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AssetClass_index : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labelException.Visible = false;
    }

    protected void gridAssetClass_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "updating the asset class");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }
}

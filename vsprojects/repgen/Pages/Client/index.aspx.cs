using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Client_index : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labelException.Visible = false;
    }

    protected void gridClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
            Guid guid = (Guid)gridClient.SelectedDataKey.Value;
            DownloadReport(guid);
        } catch (Exception ex) {
            showException(ex, labelException, "generating a report");
        }
    }
}

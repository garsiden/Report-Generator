using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : RepGenPage
{
    private static int numClients = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            this.gridClient.PageSize = numClients;

        labelException.Visible = false;
        
    }

    protected void sourceClient_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        string userId = RSMTenon.ReportGenerator.ReportGenerator.GetUserId();
        e.Result = GetRecentClients(numClients, userId);
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

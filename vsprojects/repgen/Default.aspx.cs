using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : RepGenPage
{
    private static int recentClients = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.gridClient.PageSize = recentClients;
    }
    protected void sourceClient_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = GetRecentClients(recentClients);
    }
    protected void gridClient_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Report")
        {
            var clientGuid = (Guid)gridClient.SelectedPersistedDataKey.Value;
            DownloadReport(clientGuid);
        }
    }
}

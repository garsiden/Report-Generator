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

    }

    protected void gridClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        Guid guid = (Guid)gridClient.SelectedDataKey.Value;

        DownloadReport(guid);
    }
}

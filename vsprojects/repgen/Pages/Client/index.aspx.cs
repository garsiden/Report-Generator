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
    protected void gridClient_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string guid = gridClient.SelectedPersistedDataKey.Value.ToString();
            string url = String.Format( "~/Pages/Client/edit.aspx?guid={0}", guid);
            Response.Redirect(url);
        }
            
    }
}

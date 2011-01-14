using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Client_new : RepGenPage
{
    private Guid newClientGuid = Guid.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            formView.ChangeMode(FormViewMode.Insert);
        }
        ExceptionDetails.Visible = false;
    }

    protected void formView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            ExceptionDetails.Visible = true;
            ExceptionDetails.Text = "There was a problem inserting the client. ";
            ExceptionDetails.Text += "<br/>";
            ExceptionDetails.Text += e.Exception.Message;
            e.ExceptionHandled = true;
            e.KeepInInsertMode = true;
        } else
        {
            bool existing = (bool)e.Values["ExistingAssets"];
            if (existing && newClientGuid != Guid.Empty)
            {
                string url = String.Format("~/Pages/Client/edit.aspx?guid={0}", newClientGuid);
                Response.Redirect(url);
            }
        }
    }
    protected void sourceClient_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        var rv = e.ReturnValue;
        if (rv != null)
        {
            Guid clientGuid = (Guid)rv;
            newClientGuid = clientGuid;
        }
    }
}

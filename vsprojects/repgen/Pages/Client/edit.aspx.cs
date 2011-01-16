using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using RSMTenon.Data;
using RSMTenon.ReportGenerator;

public partial class Pages_Client_edit : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExceptionDetails.Visible = false;
    }

    protected void formClient_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Display a user-friendly message
            ExceptionDetails.Visible = true;
            ExceptionDetails.Text = "There was a problem updating the client. ";
            ExceptionDetails.Text += "<br/>";
            ExceptionDetails.Text += e.Exception.Message;

            // Indicate that the exception has been handled
            e.ExceptionHandled = true;

            // Keep the row in edit mode
            e.KeepInEditMode = true;
        }
    }

    protected void btnCreateReport_Click(object sender, EventArgs e)
    {
        Guid clientGuid = new Guid(this.Request.QueryString["guid"]);
        DownloadReport(clientGuid);
    }

    protected void formClient_DataBound(object sender, EventArgs e)
    {
        FormView form = (FormView)sender;
        var client = (Client)form.DataItem;

        // set link urls
        string qs = String.Format("?guid={0}", client.GUID);

        hyperAsset.NavigateUrl += qs;
        hyperClass.NavigateUrl += qs;
    
        // set header
        this.clientHeader.InnerText = String.Format("Edit Client Details for {0}", client.Name);
    }
}

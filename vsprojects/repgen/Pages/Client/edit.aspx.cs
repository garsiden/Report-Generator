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
    private RepGenDataContext context;

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
        Client client = Client.GetClientByGUID(clientGuid);
        Report report = new Report() { Client = client };
        var repgen = new ReportGenerator();
        string tempDocName = repgen.CreateReport(report);

        try
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AppendHeader("Content-Disposition", "attachment; filename=repgen.dotx");
            Response.AppendHeader("Content-Type", "application/msword");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.TransmitFile(tempDocName);
            Response.Flush();
            Response.Close();
        }
        finally
        {
            if (File.Exists(tempDocName))
                File.Delete(tempDocName);
        }
    }
}

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
        if (!IsPostBack)
        {
            string guid = this.Request.QueryString["guid"];

            // set link urls
            string qs = String.Format("?guid={0}", guid);
            hyperAsset.NavigateUrl += qs;
            hyperClass.NavigateUrl += qs;
        }

        labelException.Visible = false;
    }

    protected void formClient_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            showException(e.Exception, labelException, "updating the client");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }

    protected void btnCreateReport_Click(object sender, EventArgs e)
    {
        Guid clientGuid = new Guid(this.Request.QueryString["guid"]);

        try {
            DownloadReport(clientGuid);
        } catch (Exception ex) {
            showException(ex, labelException, "generating a report");
        }
    }

    protected void formClient_DataBound(object sender, EventArgs e)
    {
        FormView form = (FormView)sender;
        var client = (Client)form.DataItem;

        // set header
        this.clientHeader.InnerText = String.Format("Edit Client Details for {0}", client.Name);
    }

    protected void listStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        var radio = (RadioButtonList)this.formClient.Row.FindControl("radioListStatus");

        if (((DropDownList)sender).SelectedValue == "TC")
        {
            radio.SelectedValue = "High Net Worth";
            radio.Enabled = false;
        } else
        {
            radio.Enabled = true;
        }
    }

    public void TimeHorizonServerValidate(object sender, ServerValidateEventArgs args)
    {
        try {
            var listStrategy = (DropDownList)formClient.Row.FindControl("listStrategy");
            Strategy strategy = Strategy.GetStrategies().Single(s => s.ID == listStrategy.SelectedValue);
            var listClient = (DropDownList)formClient.Row.FindControl("listTimeHorizonEdit");
            int clientTH = Int32.Parse(listClient.SelectedValue);
            args.IsValid = (clientTH >= strategy.TimeHorizon);
        } catch {
            args.IsValid = false;
        }
    }
}

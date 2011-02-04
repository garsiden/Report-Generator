using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Client_new : RepGenPage
{
    private Guid newClientGuid = Guid.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            formView.ChangeMode(FormViewMode.Insert);
        }
        labelException.Visible = false;
    }

    protected void formView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            showException(e.Exception, labelException, "adding the client");
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

    protected void listStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        var radio = (RadioButtonList)this.formView.Row.FindControl("radioListStatus");

        if (((DropDownList)sender).SelectedValue == "TC")
        {
            radio.SelectedValue = "0";
            radio.Enabled = false;
        } else
        {
            radio.Enabled = true;
        }
    }

    protected void formView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["UserId"] = RSMTenon.ReportGenerator.ReportGenerator.GetUserId();
    }

    public void TimeHorizonServerValidate(object sender, ServerValidateEventArgs args)
    {
        try {
            var listStrategy = (DropDownList)formView.Row.FindControl("listStrategy");
            string strategyId = listStrategy.SelectedValue;
            if (strategyId == "XX") {
                args.IsValid = false;
            } else {
                Strategy strategy = Strategy.GetStrategy(strategyId);
                var listClient = (DropDownList)formView.Row.FindControl("listTimeHorizonEdit");
                int clientTH = Int32.Parse(listClient.SelectedValue);
                args.IsValid = (clientTH >= strategy.TimeHorizon);
            }
        } catch {
            args.IsValid = false;
        }
    }

}

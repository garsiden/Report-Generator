using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Client_assetclasses : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string guid = Request.QueryString["guid"];
            if (guid != null)
            {
                ViewState["ClientAssetClass_ClientGUID"] = guid;
                Guid clientGuid = new Guid(guid);
                Client client = Client.GetClientByGUID(clientGuid);
                clientAssetHeader.InnerText = "Client Assets By Class for " + client.Name;

                if (client.ClientAssetClass == null) {
                    detailsView.ChangeMode(DetailsViewMode.Insert);
                }
            }
        }
        ExceptionDetails.Visible = false;
    }

    protected void sourceAssets_Inserting(object sender, LinqDataSourceInsertEventArgs e)
    {
        var asset = (ClientAssetClass)e.NewObject;

        string guidString = ViewState["ClientAssetClass_ClientGUID"].ToString();
        //Request.QueryString["guid"];
        Guid guid = new Guid(guidString);
        asset.ClientGUID = guid;
    }

    protected void sourceAssetObject_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string guidString = ViewState["ClientAssetClass_ClientGUID"].ToString();
        //Request.QueryString["guid"];
        Guid guid = new Guid(guidString);

        var asset = (ClientAssetClass)e.InputParameters["clientAsset"];
        asset.ClientGUID = guid;
    }
    protected void detailsView_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Display a user-friendly message
            ExceptionDetails.Visible = true;
            ExceptionDetails.Text = "There was a problem updating the client assets. ";
            ExceptionDetails.Text += "<br/>";
            ExceptionDetails.Text += e.Exception.Message;

            // Indicate that the exception has been handled
            e.ExceptionHandled = true;

            // Keep the row in edit mode
            e.KeepInEditMode = true;
        }

    }
    protected void detailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Display a user-friendly message
            ExceptionDetails.Visible = true;
            ExceptionDetails.Text = "There was a problem inserting the client assets. ";
            ExceptionDetails.Text += "<br/>";
            ExceptionDetails.Text += e.Exception.Message;

            // Indicate that the exception has been handled
            e.ExceptionHandled = true;

            // Keep the row in edit mode
            e.KeepInInsertMode = true;
        }

    }
}

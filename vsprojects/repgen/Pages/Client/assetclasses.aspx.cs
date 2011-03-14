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
                clientAssetHeader.InnerText = "Client Assets for " + client.Name;
                hyperClient.NavigateUrl += String.Format("?guid={0}", clientGuid);
                if (client.ClientAssetClass == null) {
                    detailsView.ChangeMode(DetailsViewMode.Insert);
                }
            }
        }
        labelException.Visible = false;
    }

    protected void sourceAssets_Inserting(object sender, LinqDataSourceInsertEventArgs e)
    {
        var asset = (ClientAssetClass)e.NewObject;

        string guidString = ViewState["ClientAssetClass_ClientGUID"].ToString();
        Guid guid = new Guid(guidString);
        asset.ClientGUID = guid;
    }

    protected void sourceAssetObject_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string guidString = ViewState["ClientAssetClass_ClientGUID"].ToString();
        Guid guid = new Guid(guidString);
        var asset = (ClientAssetClass)e.InputParameters["clientAsset"];
        asset.ClientGUID = guid;
    }

    protected void detailsView_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            showException(e.Exception, labelException, "updating the client");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }

    protected void detailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            showException(e.Exception, labelException, "adding the client");
            e.ExceptionHandled = true;
            e.KeepInInsertMode = true;
        }
    }

    protected void detailsView_ModeChanged(object sender, EventArgs e)
    {
        var mode = detailsView.CurrentMode;
        labelInstruction.Visible = mode == DetailsViewMode.Edit || mode == DetailsViewMode.Insert;
    }
}

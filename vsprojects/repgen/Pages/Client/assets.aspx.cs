using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Client_assets : RepGenPage
{
    private decimal totalAssets = 0;
    private Client client;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            var guidString = Request.QueryString["guid"];
            if (guidString != null) {
                Guid guid = new Guid(guidString);
                client = Client.GetClientByGUID(guid);
                clientAssetHeader.InnerText = "Client Assets for " + client.Name;
                hyperClient.NavigateUrl += String.Format("?guid={0}", guid);
            }

            gridAsset.DataBind();
        }

        labelException.Visible = labelExceptionDetails.Visible = false;
    }

    protected void detailsView_DataBound(object sender, EventArgs e)
    {
        var asset = (ClientAsset)((DetailsView)sender).DataItem;

        if (asset != null) {
            string name = asset.AssetName;
            this.detailsView.Caption = name;
        } else {
            detailsView.Caption = null;
        }
    }

    protected void gridAsset_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert") {
            TextBox textName = null;
            TextBox textAmount = null;

            string guidString = Request.QueryString["guid"].ToString();
            Guid clientGuid = new Guid(guidString);
            var source = e.CommandSource;

            if ((string)e.CommandArgument == "AllNew") {
                Table tbl = (Table)gridAsset.Controls[0];
                GridViewRow gvr = (GridViewRow)tbl.Controls[0];
                textName = (TextBox)gvr.FindControl("textAssetNameAddAllNew");
                textAmount = (TextBox)gvr.FindControl("textAmountAddAllNew");
            } else {
                textName = (TextBox)gridAsset.FooterRow.FindControl("textAssetNameAdd");
                textAmount = (TextBox)gridAsset.FooterRow.FindControl("textAmountAdd");
            }

            string name = textName.Text;
            decimal amount = 0;
            if (textAmount.Text != String.Empty)
                amount = Convert.ToDecimal(textAmount.Text);

            ClientAsset asset = new ClientAsset()
            {
                ClientGUID = clientGuid,
                AssetName = name,
                Amount = amount
            };

            var guid = ClientAsset.InsertClientAsset(asset);
            gridAsset.DataBind();
            int nrows = gridAsset.Rows.Count;

            if (nrows >= 1)
                gridAsset.SelectedIndex = nrows - 1;

        } else if (e.CommandName == "Delete") {
            gridAsset.DataBind();
            int nrows = gridAsset.Rows.Count;
            if (nrows > 1)
                gridAsset.SelectedIndex = 0;
            else
                gridAsset.SelectedIndex = -1;
        }
    }

    protected void detailsView_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelExceptionDetails, "updating the client assets");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }

    protected void sourceDetails_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.InputParameters[0] == null)
            e.InputParameters[0] = Guid.Empty;
    }

    protected void sourceDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        // Add ClientGUID field value to force validation
        Guid clientGuid = getClientGuidFromQueryString();
        var asset = (ClientAsset)e.InputParameters[0];
        asset.ClientGUID = clientGuid;
    }

    private Guid getClientGuidFromQueryString()
    {
        string guidString = Request.QueryString["guid"].ToString();
        if (guidString != null) {
            Guid guid = new Guid(guidString);
            return guid;
        }
        return Guid.Empty;
    }

    protected void gridAsset_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            var item = (ClientAsset)e.Row.DataItem;
            totalAssets += item.Amount;
        } else if (e.Row.RowType == DataControlRowType.Footer) {
            if (totalAssets > 0) {
                labelTotalAssets.Text = totalAssets.ToString("C0");
                labelTotalInvestment.Text = Client.InvestmentAmount.ToString("C0");
                tableInvestmentSummary.Visible = true;
                totalAssets = 0;
            }

        } else if (e.Row.RowType == DataControlRowType.EmptyDataRow) {
            totalAssets = 0;
            tableInvestmentSummary.Visible = false;
        }
    }

    private Client Client
    {
        get
        {
            if (client == null) {
                Guid clientGuid = getClientGuidFromQueryString();
                if (clientGuid != Guid.Empty)
                    client = Client.GetClientByGUID(clientGuid);
            }
            return client;
        }
    }

    protected void gridAsset_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "updating the client assets");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }

    protected void detailsView_ModeChanged(object sender, EventArgs e)
    {
        var mode = detailsView.CurrentMode;
        labelInstruction.Visible = mode == DetailsViewMode.Edit || mode == DetailsViewMode.Insert;

    }
}
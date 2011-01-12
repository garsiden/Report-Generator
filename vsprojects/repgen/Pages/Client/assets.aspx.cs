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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var guidString = Request.QueryString["guid"];
            if (guidString != null)
            {
                Guid guid = new Guid(guidString);
                Client client = Client.GetClientByGUID(guid);
                gridAsset.Caption = client.Name;
            }
        }
    }

    protected void detailsView_DataBound(object sender, EventArgs e)
    {
        var asset = (ClientAsset)((DetailsView)sender).DataItem;

        if (asset != null)
        {
            string name = asset.AssetName;
            this.detailsView.Caption = name;
            string text = String.Format("Total Asset Allocation: {0:0.0%}", asset.TotalAssetAllocation);
            this.labelAssetAllocation.Text = text;
        }
    }
    protected void gridAsset_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            TextBox textName = null;
            TextBox textAmount = null;
            
            string guidString = Request.QueryString["guid"].ToString();
            Guid clientGuid = new Guid(guidString);
            var source = e.CommandSource;

            if (e.CommandArgument == "AllNew")
            {
                Table tbl = (Table)gridAsset.Controls[0];
                GridViewRow gvr = (GridViewRow)tbl.Controls[0];
                textName = (TextBox)gvr.FindControl("textAssetNameAdd");
                textAmount = (TextBox)gvr.FindControl("textAmountAdd");
            } else {
                textName = (TextBox)gridAsset.FooterRow.FindControl("textAssetNameAdd");
                textAmount = (TextBox)gridAsset.FooterRow.FindControl("textAmountAdd");
            }
            
            string name = textName.Text;
            decimal amount = Convert.ToDecimal(textAmount.Text);

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
        }

    }
}

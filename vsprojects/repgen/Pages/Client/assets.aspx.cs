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
            TextBox textBox = null;

            
            string guidString = sourceAssetsObject.SelectParameters[0].DefaultValue;
            //string guidString2 = Request.QueryString["guid"].ToString();
            Guid clientGuid = new Guid(guidString);

            textBox = (TextBox)gridAsset.FooterRow.FindControl("textAssetNameAdd");
            string name = textBox.Text;

            textBox = (TextBox)gridAsset.FooterRow.FindControl("textAmountAdd");
            decimal amount = Convert.ToDecimal(textBox.Text);

            ClientAsset asset = new ClientAsset()
            {
                ClientGUID = clientGuid,
                AssetName = name,
                Amount = amount
            };
            ClientAsset.InsertClient(asset);
            gridAsset.DataBind();
        }

    }
}

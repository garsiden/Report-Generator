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
            ViewState["ClientAssetClass_ClientGUID"] = Request.QueryString["guid"];
        }
    }
    protected void formView_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void sourceAssets_Inserting(object sender, LinqDataSourceInsertEventArgs e)
    {
        var asset = (ClientAssetClass)e.NewObject;
       
        string guidString = ViewState["ClientAssetClass_ClientGUID"].ToString();
        //Request.QueryString["guid"];
        Guid guid = new Guid(guidString);
        asset.ClientGUID = guid;
    }
    protected void sourceAssetObject_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
       
    }
    protected void sourceAssetObject_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string guidString = ViewState["ClientAssetClass_ClientGUID"].ToString();
        //Request.QueryString["guid"];
        Guid guid = new Guid(guidString);

        var asset = (ClientAssetClass)e.InputParameters["clientAsset"];
        asset.ClientGUID = guid;
    }
    protected void sourceAssetObject_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //string guidString = ViewState["ClientAssetClass_ClientGUID"].ToString();
        ////Request.QueryString["guid"];
        //Guid guid = new Guid(guidString);

        //e.InputParameters["clientGuid"] = 
    }
}

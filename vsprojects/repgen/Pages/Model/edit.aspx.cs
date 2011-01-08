using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Model_edit : RepGenPage
{
    private decimal totalWeight = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gridModel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            TextBox textBox = null;
            ListDictionary listDictionary = new ListDictionary();

            string strategyID = sourceModel.WhereParameters[0].DefaultValue.ToString();
            listDictionary.Add("StrategyID", strategyID);

            textBox = (TextBox)gridModel.FooterRow.FindControl("textInvestmentNameAdd");
            string investment = textBox.Text;
            listDictionary.Add("InvestmentName", investment);

            DropDownList list = (DropDownList)gridModel.FooterRow.FindControl("listAssetClassAdd");
            string assetClassId = list.SelectedValue;
            listDictionary.Add("AssetClassID", assetClassId);

            textBox = (TextBox)gridModel.FooterRow.FindControl("textWeightingAdd");
            decimal weighting = Convert.ToDecimal(textBox.Text);
            listDictionary.Add("Weighting", weighting);

            textBox = (TextBox)gridModel.FooterRow.FindControl("textExpectedYieldAdd");
            decimal yield = Convert.ToDecimal(textBox.Text);
            listDictionary.Add("ExpectedYield", yield);

            textBox = (TextBox)gridModel.FooterRow.FindControl("textPurchaseChargeAdd");
            decimal charge = Convert.ToDecimal(textBox.Text);
            listDictionary.Add("PurchaseCharge", charge);
            
            sourceModel.Insert(listDictionary);
            gridModel.DataBind();
        }
    }
    protected void gridModel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            var item = (Model)e.Row.DataItem;
            totalWeight += item.Weighting;
        } else if (e.Row.RowType == DataControlRowType.Footer)
        {
            this.labelTotalWeighting.Text = String.Format("Total Weighting: {0:0.00%}", totalWeight);
        }

    }
}

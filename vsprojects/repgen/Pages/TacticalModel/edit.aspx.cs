using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_TacticalModel_edit : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            this.listStrategy.DataBind();
        }
        string id = this.listStrategy.SelectedValue;
        gridModel.Caption = String.Format("{0} Tactical Model", Strategy.GetStrategyNameFromId(id));
        labelException.Visible = false;
    }

    protected void gridModel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try {
            if (e.CommandName == "Insert") {
                TextBox textBox = null;
                ListDictionary listDictionary = new ListDictionary();

                string strategyID = listStrategy.SelectedValue;
                listDictionary.Add("StrategyID", strategyID);

                textBox = (TextBox)gridModel.FooterRow.FindControl("textInvestmentNameAdd");
                string investment = textBox.Text;
                listDictionary.Add("InvestmentName", investment);

                DropDownList list = (DropDownList)gridModel.FooterRow.FindControl("listAssetClassAdd");
                string assetClassId = list.SelectedValue;
                listDictionary.Add("AssetClassID", assetClassId);

                textBox = (TextBox)gridModel.FooterRow.FindControl("textWeightingHNWAdd");
                decimal weightingHNW = textBox.Text == String.Empty ? 0 : Convert.ToDecimal(textBox.Text);
                listDictionary.Add("WeightingHNW", weightingHNW);

                textBox = (TextBox)gridModel.FooterRow.FindControl("textWeightingAffluentAdd");
                decimal weightingAffluent = textBox.Text == String.Empty ? 0 : Convert.ToDecimal(textBox.Text);
                listDictionary.Add("WeightingAffluent", weightingAffluent);

                textBox = (TextBox)gridModel.FooterRow.FindControl("textExpectedYieldAdd");
                decimal yield = textBox.Text == String.Empty ? 0 : Convert.ToDecimal(textBox.Text);
                listDictionary.Add("ExpectedYield", yield);

                decimal charge = 0;
                listDictionary.Add("PurchaseCharge", charge);

                sourceModel.Insert(listDictionary);
                gridModel.DataBind();
            }
        } catch (Exception ex) {
            showException(ex, labelException, "adding the model entry");
        }
    }

    protected void listStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gridModel.DataBind();
    }

    protected void gridModel_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "updating the model");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }

    protected void sourceModel_Inserted(object sender, LinqDataSourceStatusEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "adding the model entry");
            labelException.Text += e.Exception.Message;
            e.ExceptionHandled = true;
        }
    }
}

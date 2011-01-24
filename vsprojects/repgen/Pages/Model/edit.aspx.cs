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
    private decimal[] fiin = { 0, 0 };
    private decimal[] loeq = { 0, 0 };
    private decimal[,] total = { { 0, 0 }, { 0, 0 }, { 0, 0 } };
    private static int HNW = 0;
    private static int AFF = 1;
    private static int TOTAL = 0;
    private static int FIIN = 1;
    private static int LOEQ = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.listStrategy.DataBind();
            string id = this.listStrategy.SelectedValue;
            gridModel.Caption = String.Format("{0} Model", Strategy.GetStrategyNameFromId(id));
            gridFixedInterest.Caption = AssetClass.GetAssetClassNameFromId("FIIN");
            gridLongEquity.Caption = AssetClass.GetAssetClassNameFromId("LOEQ");
        }
        labelException.Visible = false;
    }

    protected void gridModel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
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

            textBox = (TextBox)gridModel.FooterRow.FindControl("textPurchaseChargeAdd");
            decimal charge = textBox.Text == String.Empty ? 0 : Convert.ToDecimal(textBox.Text);
            listDictionary.Add("PurchaseCharge", charge);

            sourceModel.Insert(listDictionary);
            gridModel.DataBind();
        }
    }

    protected void gridModel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (Model)e.Row.DataItem;

            total[TOTAL, HNW] += item.WeightingHNW;
            total[TOTAL, AFF] += item.WeightingAffluent;
            total[FIIN, HNW] += item.AssetClassID == "FIIN" ? item.WeightingHNW : 0;
            total[FIIN, AFF] += item.AssetClassID == "FIIN" ? item.WeightingAffluent : 0;
            total[LOEQ, HNW] += item.AssetClassID == "LOEQ" ? item.WeightingHNW : 0;
            total[LOEQ, AFF] += item.AssetClassID == "LOEQ" ? item.WeightingAffluent : 0;
        } else if (e.Row.RowType == DataControlRowType.Footer)
        {
            this.labelTotalHNW.Text = total[TOTAL, HNW].ToString("0.00%");
            this.labelTotalAFF.Text = total[TOTAL, AFF].ToString("0.00%");
            this.labelFIIN_HNW.Text = total[FIIN, HNW].ToString("0.00%");
            this.labelFIIN_AFF.Text = total[FIIN, AFF].ToString("0.00%");
            this.labelLOEQ_HNW.Text = total[LOEQ, HNW].ToString("0.00%");
            this.labelLOEQ_AFF.Text = total[LOEQ, AFF].ToString("0.00%");

        }

    }
    protected void gridFixedInterest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (ModelBreakdown)e.Row.DataItem;
            fiin[HNW] += item.WeightingHNW;
            fiin[AFF] += item.WeightingAffluent;
        } else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = fiin[HNW].ToString("0.00%");
            e.Row.Cells[2].Text = fiin[AFF].ToString("0.00%");
        }
    }

    protected void gridLongEquity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (ModelBreakdown)e.Row.DataItem;
            loeq[HNW] += item.WeightingHNW;
            loeq[AFF] += item.WeightingAffluent;
        } else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = loeq[HNW].ToString("0.00%");
            e.Row.Cells[2].Text = loeq[AFF].ToString("0.00%");
        }
    }

    protected void listStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gridModel.DataBind();
    }

    protected void gridModel_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            labelException.Visible = true;
            labelException.Text = "There was a problem updating the model. ";
            labelException.Text += "<br/>";
            labelException.Text += e.Exception.Message;
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }
    protected void sourceModel_Inserted(object sender, LinqDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            labelException.Visible = true;
            labelException.Text = "There was a problem adding the model entry. ";
            labelException.Text += "<br/>";
            labelException.Text += e.Exception.Message;
            e.ExceptionHandled = true;
        }
    }
}

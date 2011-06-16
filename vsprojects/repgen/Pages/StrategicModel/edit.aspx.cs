using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_StrategicModel_edit : RepGenPage
{

    private decimal total = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            this.listStrategy.DataBind();
            string id = this.listStrategy.SelectedValue;
            gridModel.Caption = String.Format("{0} Strategic Model", Strategy.GetStrategyNameFromId(id));
        }
        labelException.Visible = false;
    }

    public List<ModelAssetClass> GetNewModelAssetClasses()
    {
        return GetNewModelAssetClasses(this.listStrategy.SelectedValue);
    }

    protected void listStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gridModel.DataBind();
    }

    protected void gridModel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try {
            if (e.CommandName == "Insert") {
                TextBox textBox = null;
                ListDictionary listDictionary = new ListDictionary();

                string strategyID = listStrategy.SelectedValue;
                listDictionary.Add("StrategyID", strategyID);

                DropDownList list = (DropDownList)gridModel.FooterRow.FindControl("listAssetClassAdd");
                string assetClassId = list.SelectedValue;
                listDictionary.Add("AssetClassID", assetClassId);

                textBox = (TextBox)gridModel.FooterRow.FindControl("textWeightingAdd");
                decimal weighting = textBox.Text == String.Empty ? 0 : Convert.ToDecimal(textBox.Text);
                listDictionary.Add("Weighting", weighting);

                linqSource.Insert(listDictionary);
                gridModel.DataBind();
            }
        } catch (Exception ex) {
            showException(ex, labelException, "adding the model entry");
        }

    }

    protected void gridModel_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "updating the model");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }

    protected void linqSource_Updated(object sender, LinqDataSourceStatusEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "adding the model entry");
            labelException.Text += e.Exception.Message;
            e.ExceptionHandled = true;
        }
    }

    protected void gridModel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            var item = (StrategicModel)e.Row.DataItem;
            if (item != null)
                total += item.Weighting;
        }
    }
    protected void gridModel_DataBound(object sender, EventArgs e)
    {
        this.labelTotalValue.Text = total.ToString("0.00%");
    }
}

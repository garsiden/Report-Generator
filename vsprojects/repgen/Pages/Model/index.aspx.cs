using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Model_index : RepGenPage
{
    private decimal[] sub = { 0, 0, 0 };
    private decimal[] tot = { 0, 0, 0 };
    private static int HNW = 0;
    private static int AFF = 1;
    private static int INC = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DataBind();
        }
    }

    protected void gridModel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gridChild = (GridView)e.Row.Cells[1].Controls[1];
            string assetClassId = gridModel.DataKeys[e.Row.DataItemIndex].Value.ToString();
            sourceDetail.WhereParameters[0].DefaultValue = assetClassId;
            gridChild.DataSource = sourceDetail;
            gridChild.DataBind();
        } else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Table table = (Table)(e.Row.Cells[1].FindControl("tableFooterTotal"));
            table.Rows[0].Cells[1].Text = tot[HNW].ToString("0.00%");
            table.Rows[0].Cells[2].Text = tot[AFF].ToString("0.00%");
            table.Rows[0].Cells[3].Text = (tot[INC] / 100).ToString("0.00%");
        }
    }
    protected void listStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gridModel.DataBind();
    }

    public IEnumerable<ModelAssetClass> GetAssetClasses()
    {
        string strategyId = this.listStrategy.SelectedValue;

        return Model.GetAssetClasses(strategyId);

    }

    protected void gridChild_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (Model)e.Row.DataItem;
            sub[HNW] += item.WeightingHNW;
            sub[AFF] += item.WeightingAffluent;
            sub[INC] += (100 * item.WeightingHNW * item.ExpectedYield);
        } else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Weighting Total/Average HNW Yield";
            e.Row.Cells[1].Text = sub[HNW].ToString("0.00%");
            e.Row.Cells[2].Text = sub[AFF].ToString("0.00%");
            e.Row.Cells[3].Text = (sub[INC] / (100 * sub[HNW])).ToString("0.00%");
            tot[HNW] += sub[HNW];
            tot[AFF] += sub[AFF];
            tot[INC] += sub[INC];
            sub[HNW] = sub[AFF] = sub[INC] = 0;
        }
    }
}
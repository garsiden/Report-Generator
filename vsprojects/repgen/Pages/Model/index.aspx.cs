using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Model_index : RepGenPage
{
    private decimal classWeighting = 0;
    private decimal strategyWeighting = 0;
    private decimal classIncome = 0;
    private decimal strategyIncome = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.listStrategy.DataBind();
            //this.gridModel.DataBind();
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
            table.Rows[0].Cells[1].Text = strategyWeighting.ToString("0.00%");
            table.Rows[0].Cells[2].Text = (strategyIncome / 100).ToString("0.00%");
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
            classWeighting += item.Weighting;
            classIncome += (100 * item.Weighting * item.ExpectedYield);
        } else if (e.Row.RowType == DataControlRowType.Footer) {
            e.Row.Cells[0].Text = "Weighting Total/Average Yield";
            e.Row.Cells[1].Text = classWeighting.ToString("0.00%");
            e.Row.Cells[2].Text = (classIncome / (100 * classWeighting)).ToString("0.00%");
            strategyWeighting += classWeighting;
            strategyIncome += classIncome;
            classWeighting = classIncome = 0;
        }
    }
}

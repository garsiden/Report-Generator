﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Model_index : RepGenPage
{
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
}
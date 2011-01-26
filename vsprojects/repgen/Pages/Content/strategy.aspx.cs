using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Content_strategy : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.listStrategy.DataBind();
        }
        labelStatus.Text = "";
    }

    protected void buttonAdd_Click(object sender, EventArgs e)
    {
        string strategyId = this.listStrategy.SelectedValue;

        if (strategyId != String.Empty)
        {
            try
            {
                RSMTenon.Data.Content.AddContentForStrategy(strategyId);
            } catch (Exception err)
            {
                this.listStrategy.Text = err.Message;
            }
            labelStatus.Text = "Records added";
            this.listStrategy.DataBind();
        } else
        {
            labelStatus.Text = "No Strategy to add records";
        }
    }

    protected IQueryable<Strategy> GetStrategiesWithoutContent()
    {
        return Strategy.GetStrategiesWithoutContent();
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Content_index : RepGenPage
{
    private int rowCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.listStrategy.DataBind();
            this.listContentTag.DataBind();
        }
        labelException.Visible = false;
    }

    protected void gridContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            rowCount++;
        else
        {
            this.labelCount.Text = rowCount.ToString();
        }
    }

    protected void gridContent_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            labelException.Visible = true;
            labelException.Text = "There was a problem deleteing the Text Content. ";
            labelException.Text += "<br/>";
            labelException.Text += e.Exception.InnerException.Message;
            e.ExceptionHandled = true;
        }
    }

    protected void gridContent_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            labelException.Visible = true;
            labelException.Text = "There was a problem updating the Text Content. ";
            labelException.Text += "<br/>";
            labelException.Text += e.Exception.InnerException.Message;
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }

    }
}

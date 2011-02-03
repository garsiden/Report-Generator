using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AssetClass_editN_Z : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labelException.Visible = false;
    }

    protected void gridHistoricData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try {
            if (e.CommandName == "Insert") {

                string[] fields = { "Date", "CASH", "COMM", "COPR", "GLEQ", "HEDG", "LOSH" };
                ListDictionary listDictionary = new ListDictionary();

                foreach (var f in fields) {
                    string boxName = "text" + f + "Add";
                    TextBox textBox = (TextBox)gridHistoricData.FooterRow.FindControl(boxName);
                    if (f == "Date") {
                        DateTime dt = DateTime.Parse(textBox.Text);
                        listDictionary.Add(f, dt);
                    } else {
                        double db = Double.Parse(textBox.Text);
                        listDictionary.Add(f, db);
                    }
                    textBox.Text = String.Empty;
                }

                sourceHistoricData.Insert(listDictionary);
                gridHistoricData.DataBind();
            }
        } catch (Exception ex) {
            showException(ex, labelException, "adding the asset class prices");
        }

    }
    protected void sourceHistoricData_Inserted(object sender, LinqDataSourceStatusEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "adding the assets class prices");
            e.ExceptionHandled = true;
        }
    }

    protected void gridHistoricData_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "deleting the asset class prices");
            e.ExceptionHandled = true;
        }
    }

    protected void gridHistoricData_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) {
            showException(e.Exception, labelException, "updating the asset class prices");
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
    }
}

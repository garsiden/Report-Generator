using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AssetClass_editA_M : RepGenPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gridHistoricData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert") {

            string[] fields = { "Date", "PREQ", "UKCB", "UKGB", "UKHY", "UKEQ", "WOBO" };
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
    }
}

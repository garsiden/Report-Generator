using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSMTenon.Data;

public partial class Pages_Report : System.Web.UI.Page
{
    private RepGenDataContext context;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //var bdp = (BasicFrame.WebControls.BDPLite)formClient.FindControl("dpMeetingInsert");
            //bdp.SelectedDate = DateTime.Today;
        }

    }

    public IEnumerable<Strategy> GetStrategies()
    {
        return DataContext.Strategies;
    }


    private RepGenDataContext DataContext
    {
        get
        {
            if (context == null)
            {
                context = new RepGenDataContext();
            }
            return context;
        }
    }
    protected void InvestmentAmountTextBox_TextChanged(object sender, EventArgs e)
    {
        string text = ((TextBox)sender).Text;

        int amount = Int32.Parse(text);

        TextBox box = (TextBox)this.formClient.FindControl("ReportingFrequencyTextBox");

        if (amount >= 1000000)
        {
            box.Text = "quarterly";
        }else {

            box.Text = "half yearly";
        }
    }
    protected void sourceClient_Inserting(object sender, LinqDataSourceInsertEventArgs e)
    {
        ((Client)e.NewObject).GUID = Guid.NewGuid();
    }
}

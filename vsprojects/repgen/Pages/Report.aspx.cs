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
        ExceptionDetails.Visible = false;

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

    protected void formClient_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Display a user-friendly message
            ExceptionDetails.Visible = true;
            ExceptionDetails.Text = "There was a problem updating the client. ";
            ExceptionDetails.Text += "<br/>";
            ExceptionDetails.Text += e.Exception.Message;

            // Indicate that the exception has been handled
            e.ExceptionHandled = true;

            // Keep the row in edit mode
            e.KeepInEditMode = true;
        }
    }
}

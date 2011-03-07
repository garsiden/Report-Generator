using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

public partial class UserControls_SiteHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            var user = HttpContext.Current.User;
            var principal = (WindowsIdentity)user.Identity;
            userLabel.Text = String.Format("User: {0}", principal.Name.Split('\\')[1]);
            if (user.IsInRole(RSMTenon.ReportGenerator.ReportGenerator.AdminGroup)) {
                roleLabel.Text = "Administrator";
                roleLabel.Visible = true;
            }
        }
    }
}

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
            var principal = (WindowsIdentity)HttpContext.Current.User.Identity;
            this.greetingLabel.Text = String.Format("User: {0}", principal.Name.Split('\\')[1]);
            foreach (var group in principal.Groups)
                greetingLabel.Text += String.Format("<br />{0}", group.Translate(typeof(NTAccount)));
        }
    }
}

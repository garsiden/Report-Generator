using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Web.Configuration;

public partial class Pages_Template_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DataBind();
            this.dropList.SelectedValue = GetSelectedTemplate();
        }
    }

    public FileInfo[] GetTemplates()
    {
        string templateDir = Server.MapPath("~/App_Data/templates");
        DirectoryInfo info = new DirectoryInfo(templateDir);
        FileInfo[] templates = info.GetFiles("*.dotx");

        return templates;

    }

    public string GetSelectedTemplate()
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration("~/");

        string template = config.AppSettings.Settings["TemplateFile"].Value;

        return template;

    }

    protected void btnSetTemplate_Click(object sender, EventArgs e)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration("~/");

        string current = GetSelectedTemplate();
        string selected = this.dropList.SelectedValue.ToString();
        if (current != selected)
        {
            config.AppSettings.Settings["TemplateFile"].Value = selected;
            config.Save();
        }
    }
}

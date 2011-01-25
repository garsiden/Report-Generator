using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

public partial class Pages_Content_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (uploader.PostedFile.ContentLength != 0)
        {
            try
            {
                if (uploader.PostedFile.ContentLength > 1000000)
                {
                    lblStatus.Text = "File is too large for upload";
                } else
                {
                    string destPath = Server.MapPath("~/App_Data/content_test.xml");

                    // try opening as Xml file
                    using (StreamReader sr = new StreamReader(uploader.PostedFile.InputStream))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(sr);
                        doc.Save(destPath);

                    }
                    lblStatus.Text = String.Format("File {0} uploaded", uploader.PostedFile.FileName);
                }
            } catch (Exception err)
            {
                lblStatus.Text = err.Message;
            }
        }
    }
}

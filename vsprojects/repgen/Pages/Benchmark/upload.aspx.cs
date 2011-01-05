using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Pages_Benchmark_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (uploader.PostedFile.ContentLength != 0) {
            try {
                if (uploader.PostedFile.ContentLength > 100000) {
                    lblStatus.Text = "File is too large for upload";
                } else {
                    string destDir = Server.MapPath("~/App_Data/uploads");
                    string filename = Path.GetFileName(uploader.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, filename);
                    uploader.PostedFile.SaveAs(destPath);
                }
            } catch (Exception err){
                lblStatus.Text = err.Message;
            }
        }
    }
}

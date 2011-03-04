using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;
using RSMTenon.Data;

public abstract class UploadPage : System.Web.UI.Page
{
    protected DataTable Table { get; set; }
    protected string TableName { get; set; }

    protected void Upload(FileUpload uploader, Label lblStatus)
    {
        if (uploader.PostedFile.ContentLength != 0) {
            try {
                if (uploader.PostedFile.ContentLength > 100000) {
                    lblStatus.Text = "File is too large for upload";
                } else {
                    using (StreamReader sr = new StreamReader(uploader.PostedFile.InputStream)) {
                        string line = null;
                        string[] split = null;
                        char[] sep = { ',' };
                        var headers = sr.ReadLine().Split(sep).Skip(1);
                        while ((line = sr.ReadLine()) != null) {
                            split = line.Split(sep);
                            AddToTypedTable(split, headers);
                        }
                    }
                    RSMTenon.Data.DataUtilities.UploadToDatabase(Table, TableName, null);
                    lblStatus.Text = String.Format("{0:#,##0} row(s) added to database", Table.Rows.Count);
                }
            } catch (Exception err) {
                lblStatus.Text = err.Message;
            }
        }
    }

    protected abstract void AddToTypedTable(string[] fields, IEnumerable<string> headers);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class Pages_Benchmark_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            lblStatus.Text = "";
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        var dt = new DataUpload.BenchmarkDataTable();

        if (uploader.PostedFile.ContentLength != 0)
        {
            try
            {
                if (uploader.PostedFile.ContentLength > 100000)
                {
                    lblStatus.Text = "File is too large for upload";
                } else
                {
                    using (StreamReader sr = new StreamReader(uploader.PostedFile.InputStream))
                    {
                        string line = null;
                        string[] split = null;
                        char[] sep = { ',' };
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith("Date"))
                            {
                                continue;
                            } else
                            {
                                split = line.Split(sep);
                                addToTypedTable(dt, split);
                            }
                        }
                    }
                    RSMTenon.Data.DataUtilities.UploadToDatabase(dt, "tblBenchmarkData");
                    lblStatus.Text = String.Format("{0:#,##0} row(s) added to database", dt.Rows.Count);
                }
            } catch (Exception err)
            {
                lblStatus.Text = err.Message;
            }
        }

    }

    private void addToTypedTable(DataUpload.BenchmarkDataTable dt, string[] fields)
    {
        DataUpload.BenchmarkRow row = dt.NewBenchmarkRow();

        row.Date = DateTime.ParseExact(fields[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
        row.STBO = Convert.ToDouble(fields[1]);
        row.CAMA = Convert.ToDouble(fields[2]);
        row.BAMA = Convert.ToDouble(fields[3]);
        row.ACMA = Convert.ToDouble(fields[4]);
        row.GLGR = Convert.ToDouble(fields[5]);
        dt.AddBenchmarkRow(row);
    }


}

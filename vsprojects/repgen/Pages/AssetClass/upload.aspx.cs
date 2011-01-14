using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class Pages_AssetClass_upload : System.Web.UI.Page
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
        var dt = new DataUpload.HistoricDataTable();

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
                            if (line.Contains("Date"))
                            {
                                continue;
                            } else
                            {
                                split = line.Split(sep);
                                addToTypedTable(dt, split);
                            }
                        }
                    }
                    RSMTenon.Data.DataUtilities.UploadToDatabase(dt, "tblHistoricData");
                    lblStatus.Text = String.Format("{0:#,##0} row(s) added to database", dt.Rows.Count);
                }
            } catch (Exception err)
            {
                lblStatus.Text = err.Message;
            }
        }

    }

    private void addToTypedTable(DataUpload.HistoricDataTable dt, string[] fields)
    {
        DataUpload.HistoricRow row = dt.NewHistoricRow();

        row.Date = DateTime.ParseExact(fields[0].Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        row.CASH = Convert.ToDouble(fields[1]);
        row.COMM = Convert.ToDouble(fields[2]);
        row.COPR = Convert.ToDouble(fields[3]);
        row.GLEQ = Convert.ToDouble(fields[4]);
        row.HEDG = Convert.ToDouble(fields[5]);
        row.LOSH = Convert.ToDouble(fields[6]);
        row.PREQ = Convert.ToDouble(fields[7]);
        row.UKCB = Convert.ToDouble(fields[8]);
        row.UKEQ = Convert.ToDouble(fields[9]);
        row.UKGB = Convert.ToDouble(fields[10]);
        row.UKHY = Convert.ToDouble(fields[11]);
        row.WOBO = Convert.ToDouble(fields[12]);
        dt.AddHistoricRow(row);
    }


}

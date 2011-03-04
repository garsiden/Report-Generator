using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;
using RSMTenon.Data;

public partial class Pages_AssetClass_upload : UploadPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Text = String.Empty;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        TableName = "tblHistoricData";
        Table = new DataUpload.HistoricDataTable();
        Upload(uploader, lblStatus);
    }

    protected override void AddToTypedTable(string[] fields, IEnumerable<string> headers)
    {
        var dt = (DataUpload.HistoricDataTable)Table;
        DataUpload.HistoricRow row = dt.NewHistoricRow();

        row.Date = DateTime.ParseExact(fields[0].Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        int idx = 1;

        foreach (var hdr in headers)
            row[hdr] = Convert.ToDouble(fields[idx++]);

        dt.AddHistoricRow(row);
    }
}
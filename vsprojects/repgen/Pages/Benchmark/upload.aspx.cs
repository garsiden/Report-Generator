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

public partial class Pages_Benchmark_upload : UploadPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Text = String.Empty;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        TableName = "tblBenchmarkData";
        Table = new DataUpload.BenchmarkDataTable();
        Upload(uploader, lblStatus);
    }

    protected override void AddToTypedTable(string[] fields, IEnumerable<string> headers)
    {
        var dt = (DataUpload.BenchmarkDataTable)Table;
        DataUpload.BenchmarkRow row = dt.NewBenchmarkRow();

        row.Date = DateTime.ParseExact(fields[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
        int idx = 1;

        foreach (var hdr in headers)
            row[hdr] = Convert.ToDouble(fields[idx++]);

        dt.AddBenchmarkRow(row);
    }
}
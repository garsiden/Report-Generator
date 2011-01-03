using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using RSMTenon.Data;
using RSMTenon.ReportGenerator;

public partial class RepGenPage : System.Web.UI.Page

{
    protected RepGenDataContext context;

    public IEnumerable<Strategy> GetStrategies()
    {
        return DataContext.Strategies;
    }

    public IEnumerable<Benchmark> GetBenchmarks()
    {
        return DataContext.Benchmarks.OrderBy(b => b.Name);
    }

    protected RepGenDataContext DataContext
    {
        get
        {
            if (context == null)
            {
                context = new RepGenDataContext();
            }
            return context;
        }
    }

    protected void DownloadReport(Guid clientGuid)
    {
        Client client = Client.GetClientByGUID(clientGuid);
        Report report = new Report() { Client = client };
        var repgen = new ReportGenerator();
        string tempDocName = repgen.CreateReport(report);

        try
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AppendHeader("Content-Disposition", "attachment; filename=repgen.dotx");
            Response.AppendHeader("Content-Type", "application/msword");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.TransmitFile(tempDocName);
            Response.Flush();
            Response.Close();
        }
        finally
        {
            if (File.Exists(tempDocName))
                File.Delete(tempDocName);
        }
    }
}

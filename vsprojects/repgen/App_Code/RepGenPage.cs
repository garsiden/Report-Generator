using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    private Dictionary<string, AssetClass> assetClassDictionary = null;

    public List<Strategy> GetStrategies()
    {
        return Strategy.GetStrategies();
    }

    public List<Benchmark> GetBenchmarks()
    {
        return DataContext.Benchmarks.OrderBy(b => b.Name).ToList();
    }

    public List<AssetClass> GetModelAssetClasses()
    {
        var classes = from cls in DataContext.AssetClasses
                      join grp in DataContext.AssetGroupClasses
                      on cls.ID equals
                          grp.AssetClassID
                      into all
                      from a in all.DefaultIfEmpty()
                      where a.AssetClassID == null
                      select cls;

        return classes.ToList();
    }

    public List<AssetClass> GetBreakdownAssetClasses()
    {
        var classes = from cls in DataContext.AssetClasses
                      join grp in DataContext.AssetGroupClasses
                      on cls.ID equals
                          grp.AssetClassID
                      orderby cls.Name
                      select cls;

        return classes.ToList();
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
        } finally
        {
            if (File.Exists(tempDocName))
                File.Delete(tempDocName);
        }
    }

    protected ListDictionary CreateListDictionaryFromGridFooter(string[] fields, GridView gridView)
    {
        var listDictionary = new ListDictionary();

        foreach (var f in fields)
        {
            string boxName = "text" + f + "Add";
            TextBox textBox = (TextBox)gridView.FooterRow.FindControl(boxName);
            if (f == "Date")
            {
                DateTime dt = DateTime.Parse(textBox.Text);
                listDictionary.Add(f, dt);
            } else
            {
                double db = Double.Parse(textBox.Text);
                listDictionary.Add(f, db);
            }
            textBox.Text = String.Empty;
        }

        return listDictionary;
    }

    public string GetAssetClassName(string id)
    {
        if (assetClassDictionary == null)
            assetClassDictionary = DataContext.AssetClasses.ToDictionary(a => a.ID);

        AssetClass asset;
        if (assetClassDictionary.TryGetValue(id, out asset))
            return asset.Name;
        else
            return string.Empty;
    }
}

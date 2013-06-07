using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

using RSMTenon.Data;
using RSMTenon.Graphing;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using W = DocumentFormat.OpenXml.Drawing.Wordprocessing;

namespace RSMTenon.ReportGenerator
{
    public class ReportGenerator
    {
        private static string sourceDir = @"~/App_Data/";
        private uint docPrId = 0;

        private string TempDir { get; set; }
        private string TemplateFile { get; set; }
        private string ContentXmlFile { get; set; }
        private string TempContentXmlFile { get; set; }
        private string ReportSpecFile { get; set; }
        public Client Client { get; set; }
        public Report Report { get; set; }
        public bool FillContentControls { get; set; }

        public ReportGenerator(Client client)
        {

            // get values from web.config
            // template
            NameValueCollection appSettings = (NameValueCollection)ConfigurationManager.GetSection("appSettings");
            string templateFile = appSettings["TemplateFile"];
            TemplateFile = HttpContext.Current.Server.MapPath(sourceDir + "templates\\" + templateFile);
            //content file
            string contentFile = appSettings["ContentFile"];
            ContentXmlFile = HttpContext.Current.Server.MapPath(sourceDir + contentFile);
            //temp directory
            string tempDir = appSettings["TempDir"];

            if (tempDir != null && Directory.Exists(tempDir))
                TempDir = tempDir;
            else
                TempDir = Environment.GetEnvironmentVariable("temp");

            // source files
            TempContentXmlFile = tempDir + "\\content_temp.xml";
            ReportSpecFile = HttpContext.Current.Server.MapPath(sourceDir + "report-spec.xml");

            // directly fill Content Controls?
            FillContentControls = Boolean.Parse(appSettings["FillContentControls"] ?? "true");

            Client = client;
            Report = new Report(ReportSpecFile) { Client = Client };
        }

        public string CreateReport()
        {
            // check client assets; throw error on failure
            Report.Client.ValidateClientAssets();

            string tempDocName = null;
            WordprocessingDocument myWordDoc = null;
            MainDocumentPart mainPart = null;
            MemoryStream tempXmlStream = null;

            try {
                // create temp files/streams
                tempDocName = createTempDocFile(TemplateFile, TempDir);
                tempXmlStream = createTempXmlStream(ContentXmlFile);

                // open Word document
                myWordDoc = WordprocessingDocument.Open(tempDocName, true);
                mainPart = myWordDoc.MainDocumentPart;

                // Add text content
                TextContent tc = new TextContent(Report);
                tc.GenerateTextContent(mainPart, ContentXmlFile, tempXmlStream, FillContentControls);

                // Model Table
                string controlName = null;
                Table table = Report.ModelTable();
                controlName = "ModelTable";
                AddTableToDoc(mainPart, table, controlName);

                // Charts
                AddChartsToDoc(mainPart, Report);
            } finally {
                // save and close document
                if (tempXmlStream != null)
                    tempXmlStream.Close();
                if (mainPart != null)
                    mainPart.Document.Save();
                if (myWordDoc != null)
                    myWordDoc.Close();
                if (tempXmlStream != null)
                    tempXmlStream.Close();
            }

            return tempDocName;
        }

        private void AddChartsToDoc(MainDocumentPart mainPart, Report report)
        {
            string controlName = null;
            ChartItem chartItem = null;

            // Allocation Pie Chart
            chartItem = report.Allocation();
            AddChartToDoc(mainPart, chartItem);

            // Comparison Chart
            if (report.Client.ExistingAssets) {
                chartItem = report.AllocationComparison();
                AddChartToDoc(mainPart, chartItem);
            } else {
                controlName = report.GetContentControlNameForChart("allocation-comparison");
                RemoveContentControlFromBlock(mainPart, controlName);
            }

            // Drawdown
            chartItem = report.Drawdown();
            AddChartToDoc(mainPart, chartItem);

            // Ten Year Return Chart
            chartItem = report.TenYearReturn();
            AddChartToDoc(mainPart, chartItem);

            // Stress Test Market Rise Bar Chart
            chartItem = report.StressTestMarketRise();
            AddChartToDoc(mainPart, chartItem);

            // Stress Test Market Crash Bar Chart
            chartItem = report.StressTestMarketCrash();
            AddChartToDoc(mainPart, chartItem);

            // Rolling Return 5 yr
            chartItem = report.RollingReturnChart(5);
            AddChartToDoc(mainPart, chartItem);
        }

        private void AddChartToDoc(MainDocumentPart mainPart, ChartItem chartItem)
        {
            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, chartItem.CustomControlName);

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);

            GraphData gd = chartItem.GraphData;
            gd.Close();

            gd.AddEmbeddedToChartPart(chartPart);
            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpaceWithData(chartItem.Chart, gd.ExternalDataId);
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            uint prId = getNextDocPrId(mainPart);

            Drawing drawing = GraphDrawing.GenerateDrawing(relId, chartItem.CustomControlName, prId, chartItem.Cx, chartItem.Cy);
            para.Append(run);
            run.Append(drawing);
        }

        private void AddTableToDoc(MainDocumentPart mainPart, Table table, string controlName)
        {
            // open document and create table
            Document doc = mainPart.Document;

            // create table to hold strategy details
            // add table to doc and save
            List<SdtBlock> stdList =
                mainPart.Document.Descendants<SdtBlock>()
                .Where(s => controlName
                .Contains
                (s.SdtProperties.GetFirstChild<SdtAlias>().Val.Value)).ToList();

            if (stdList.Count != 0) {
                SdtBlock sdt = stdList.First<SdtBlock>();
                OpenXmlElement parent = sdt.Parent;
                parent.InsertAfter(table, sdt);
                sdt.Remove();
            }

            doc.Save();
        }

        private Paragraph findAndRemoveContent(MainDocumentPart main, string blockName)
        {
            SdtBlock sdt = main.Document.Descendants<SdtBlock>().Where(
                 s => s.SdtProperties.GetFirstChild<SdtAlias>().Val.Value.Equals(blockName)).First();

            if (sdt != null) {
                Paragraph para = sdt.SdtContentBlock.GetFirstChild<Paragraph>();
                para.RemoveAllChildren();
                return para;
            }
            return null;
        }

        private static string createTempDocFile(string templateFile, string tempDir)
        {
            string tempFile = String.Format("{0}\\{1}.dotx", tempDir, Guid.NewGuid().ToString());
            copyFile(templateFile, tempFile);

            return tempFile;
        }

        private MemoryStream createTempXmlStream(string source)
        {
            int buflen = 16384;
            MemoryStream output = null;

            using (FileStream input = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                output = new MemoryStream(buflen);
                byte[] buffer = new byte[buflen];
                int len;
                while ((len = input.Read(buffer, 0, buffer.Length)) > 0) {
                    output.Write(buffer, 0, len);
                }
            }
            output.Position = 0;
            return output;
        }

        private static void copyFile(string source, string destination)
        {
            using (FileStream input = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                using (FileStream output = new FileStream(destination, FileMode.Create, FileAccess.Write)) {
                    byte[] buffer = new byte[16384];
                    int len;
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0) {
                        output.Write(buffer, 0, len);
                    }
                }
            }
        }

        public static string GetUserId()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static bool RemoveContentControlFromRun(MainDocumentPart mainPart, string controlAlias)
        {
            var sdts = mainPart.Document.Descendants<SdtRun>();
            bool removed = false;
            foreach (var sdt in sdts) {
                var alias = sdt.Descendants<SdtAlias>().FirstOrDefault();
                if ((alias != null) && (alias.Val != null) &&
                  (alias.Val.HasValue) && (alias.Val.Value == controlAlias)) {
                    sdt.Remove();
                    removed = true;
                }
            }

            return removed;
        }

        public static bool RemoveContentControlFromBlock(MainDocumentPart mainPart, string controlAlias)
        {
            var sdts = mainPart.Document.Descendants<SdtBlock>();
            bool removed = false;
            foreach (var sdt in sdts) {
                var alias = sdt.Descendants<SdtAlias>().FirstOrDefault();
                if ((alias != null) && (alias.Val != null) &&
                  (alias.Val.HasValue) && (alias.Val.Value == controlAlias)) {
                    sdt.Remove();
                    removed = true;
                }
            }

            return removed;
        }

        private uint getNextDocPrId(MainDocumentPart mainPart)
        {
            if (docPrId == 0)
                docPrId = mainPart.Document.Descendants<W.DocProperties>().Max(dp => dp.Id.Value);

            return ++docPrId;
        }

        public static string AdminGroup
        {
            get
            {
                return ConfigurationManager.AppSettings["AdminGroup"];
            }
        }
    }
}

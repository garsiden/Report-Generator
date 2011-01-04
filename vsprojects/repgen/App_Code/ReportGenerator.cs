using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Configuration;

using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace RSMTenon.ReportGenerator
{
    public class ReportGenerator : System.Web.UI.Page
    {
        private static string sourceDir = @"~/App_Data/";
        private string TempDir { get; set; }

        private string TemplateFile { get; set; }
        private string ContentXmlFile { get; set; }
        private string TempContentXmlFile { get; set; }

        public ReportGenerator()
        {
            NameValueCollection appSettings = (NameValueCollection)ConfigurationManager.GetSection("appSettings");
            string templateFile = appSettings["TemplateFile"];
            TemplateFile = Server.MapPath(sourceDir + templateFile);
            string contentFile = appSettings["ContentFile"];
            ContentXmlFile = Server.MapPath(sourceDir + contentFile);
            string tempDir = appSettings["TempDir"];
            if (tempDir == null)
                tempDir = Environment.GetEnvironmentVariable("temp");
            TempDir = tempDir;
            TempContentXmlFile = tempDir + "\\content_temp.xml";
        }

        public string CreateReport(Report report)
        {
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
                TextContent tc = new TextContent();
                tc.GenerateTextContent(mainPart, report.Client, ContentXmlFile, tempXmlStream);

                // Model Table
                string controlName = null;
                Table table = report.ModelTable();
                controlName = "ModelTable";
                AddTableToDoc(mainPart, table, controlName);

                // Charts
                AddChartsToDoc(mainPart);
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

        private void AddChartsToDoc(MainDocumentPart mainPart)
        {

            //string controlName = null;
            //ChartItem chartItem = null;
            //// Drawdown
            //chartItem = report.Drawdown();
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);

            //// Comparison Chart
            //if (report.Client.ExistingAssets)
            //{
            //    chartItem = report.AllocationComparison();
            //    controlName = chartItem.CustomControlName;
            //    AddChartToDoc(mainPart, chartItem, controlName);
            //}

            //// Stress Test Market Rise Bar Chart
            //chartItem = report.StressTestMarketRise();
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);

            //// Stress Test Market Crash Bar Chart
            //chartItem = report.StressTestMarketCrash();
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);

            //// Allocation Pie Chart
            //chartItem = report.Allocation();
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);

            //// Rolling Return 1 yr
            //chartItem = report.RollingReturnChart(1);
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);

            //// Rolling Return 3 yr
            //chartItem = report.RollingReturnChart(3);
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);

            //// Rolling Return 5 yr
            //chartItem = report.RollingReturnChart(5);
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);

            //// Ten Year Return Chart
            //chartItem = report.TenYearReturn();
            //controlName = chartItem.CustomControlName;
            //AddChartToDoc(mainPart, chartItem, controlName);
        }

        private void AddChartToDoc(MainDocumentPart mainPart, ChartItem chartItem, string controlName)
        {
            //// open Word documant and remove existing content from control
            //Paragraph para = findAndRemoveContent(mainPart, controlName);

            //// generate new ChartPart and ChartSpace
            //ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            //string relId = mainPart.GetIdOfPart(chartPart);
            //C.ChartSpace chartSpace = GraphSpace.GenerateChartSpace(chartPart);
            //chartSpace.Append(chartItem.Chart);

            //// set ChartPart ChartSpace
            //chartPart.ChartSpace = chartSpace;

            //// generate a new Wordprocessing Drawing, add to a new Run,
            //// and relate to new ChartPart
            //Run run = new Run();
            //Drawing drawing = GraphDrawing.GenerateDrawing(relId, controlName, docPrId, Graph.Cx, Graph.Cy);
            //docPrId++;
            //para.Append(run);
            //run.Append(drawing);
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
                //sdt.Remove();
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
    }
}

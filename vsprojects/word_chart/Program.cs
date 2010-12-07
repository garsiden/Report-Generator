using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using RSMTenon.Graph;

namespace RSMTenon.ReportGenerator
{
    class Program
    {
        static string path = @"C:\Documents and Settings\garsiden\My Documents\Projects\RepGen\test\";
        static string generated = path + "TableTest3.docx";

        static void Main(string[] args)
        {
            Program This = new Program();
            var myData = new Dictionary<string, decimal>();
            myData.Add("Cash", 0.015M);
            myData.Add("Fixed Interest", 0.452M);
            myData.Add("Hedge", 0.14M);
            myData.Add("Real Estate", 0.15M);
            myData.Add("Long Short Equities", 0.12M);
            myData.Add("Long Equities", 0.09M);
            myData.Add("Commodities", 0.033M);

            string template = path + "PieChart.docx";
            string generated = path + "LineChartWord.docx";

            // copy template to generated
            File.Copy(template, generated, true);

            // open Word document
            WordprocessingDocument myWordDoc = WordprocessingDocument.Open(generated, true);
            MainDocumentPart mainPart = myWordDoc.MainDocumentPart;

            This.InsertLineChartIntoWord(mainPart, "1yr Rolling return", myData);

            myWordDoc.Close();
        }

        public void InsertChartIntoWord(MainDocumentPart mainPart, string title, Dictionary<string,decimal>data)
        {
            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, "Chart1");

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);
            C.ChartSpace chartSpace = GenerateChartSpace(chartPart);

            // generate Pie Chart and add to ChartSpace
            AllocationPieChart pie = new AllocationPieChart();
            C.Chart chart = pie.GenerateChart(title, data);
            chartSpace.Append(chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            Drawing drawing = GenerateDrawing(relId);
            para.Append(run);
            run.Append(drawing);

            // save and close document
            mainPart.Document.Save();
        }

        public void InsertLineChartIntoWord(MainDocumentPart mainPart, string title, Dictionary<string, decimal> data)
        {
            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, "Chart1");

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);
            C.ChartSpace chartSpace = GenerateChartSpace(chartPart);

            // generate Line Chart and add to ChartSpace
            RollingReturnLineChart rrlc = new RollingReturnLineChart();
            C.Chart chart = rrlc.GenerateChart(title, data);
            chartSpace.Append(chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            Drawing drawing = GenerateDrawing(relId);
            para.Append(run);
            run.Append(drawing);

            // save and close document
            mainPart.Document.Save();
        }

        public Drawing GenerateDrawing(string id)
        {
            Drawing drawing1 = new Drawing();

            Wp.Inline inline1 = new Wp.Inline();
            Wp.Extent extent1 = new Wp.Extent() { Cx = AllocationPieChart.Cx, Cy = AllocationPieChart.Cy };
            Wp.DocProperties docProperties1 = new Wp.DocProperties() { Id = (UInt32Value)2U, Name = "Chart 1" };

            A.Graphic graphic1 = new A.Graphic();
            graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.GraphicData graphicData1 = new A.GraphicData() { Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" };

            C.ChartReference chartReference1 = new C.ChartReference() { Id = id };
            chartReference1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartReference1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            graphicData1.Append(chartReference1);

            graphic1.Append(graphicData1);

            inline1.Append(extent1);
            inline1.Append(docProperties1);
            inline1.Append(graphic1);

            drawing1.Append(inline1);
            return drawing1;
        }

        private C.ChartSpace GenerateChartSpace(ChartPart part)
        {
            // c:chartSpace (ChartSpace)            
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            // c:lang (EditingLanguage)
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-GB" };

            // c:printSettings (PrintSettings)
            C.PrintSettings printSettings1 = new C.PrintSettings();
            C.HeaderFooter headerFooter1 = new C.HeaderFooter();
            C.PageMargins pageMargins1 = new C.PageMargins() { Left = 0.70D, Right = 0.70D, Top = 0.75D, Bottom = 0.75D, Header = 0.30D, Footer = 0.30D };
            C.PageSetup pageSetup1 = new C.PageSetup();

            printSettings1.Append(headerFooter1);
            printSettings1.Append(pageMargins1);
            printSettings1.Append(pageSetup1);

            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(printSettings1);

            return chartSpace1;
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

    }
}

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
using RSMTenon.Graphing;

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
            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpace(chartPart);

            // generate Pie Chart and add to ChartSpace
            AllocationPieChart pie = new AllocationPieChart();
            C.Chart chart = pie.GenerateChart(title, data);
            chartSpace.Append(chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            Drawing drawing = GraphDrawing.GenerateDrawing(relId, "Chart 1", 2U, AllocationPieChart.Cx, AllocationPieChart.Cy);
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
            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpace(chartPart);

            // generate Line Chart and add to ChartSpace
            RollingReturnLineChart rrlc = new RollingReturnLineChart();
            C.Chart chart = rrlc.GenerateChart(title, data);
            chartSpace.Append(chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            Drawing drawing = GraphDrawing.GenerateDrawing(relId, "Chart 2", 3U, RollingReturnLineChart.Cx, RollingReturnLineChart.Cy);
            para.Append(run);
            run.Append(drawing);

            // save and close document
            mainPart.Document.Save();
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

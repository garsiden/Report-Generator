using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;

namespace RSMTenon.Graphing
{
    public class ExcelGraph  : Graph
    {
        public Chart GenerateChart(string title)
        {
            Chart chart = new Chart();
            GenerateContent(chart, title);

            return chart;
        }

        // Adds child parts and generates content of the specified part.
        public static void AddEmbeddedToChartPart(ChartPart part, string externalDataId, Stream ms)
        {
            EmbeddedPackagePart embeddedPackagePart1 = part.AddNewPart<EmbeddedPackagePart>("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", externalDataId);
            GenerateEmbeddedPackagePart1Content(embeddedPackagePart1, ms);

           // GeneratePartContent(part);

        }

        // Generates content of embeddedPackagePart1.
        private static void GenerateEmbeddedPackagePart1Content(EmbeddedPackagePart embeddedPackagePart1)
        {
            System.IO.Stream data = GetBinaryDataStream(embeddedPackagePart1Data);
            embeddedPackagePart1.FeedData(data);
            data.Close();
        }

        private static void GenerateEmbeddedPackagePart1Content(EmbeddedPackagePart embeddedPackagePart1, Stream ms)
        {
            ms.Position = 0;
            //System.IO.Stream data = GetBinaryDataStream(embeddedPackagePart1Data);
            embeddedPackagePart1.FeedData(ms);
            ms.Close();
        }

        // Generates content of part.
        private void GenerateContent(Chart chart1, string title)
        {
            //C.ChartSpace chartSpace1 = new C.ChartSpace();
            //chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            //chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            //chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            //C.Date1904 date19041 = new C.Date1904(){ Val = true };
            //C.EditingLanguage editingLanguage1 = new C.EditingLanguage(){ Val = "en-US" };

            //C.Chart chart1 = new C.Chart();
            C.Title title1 = new C.Title();

            C.View3D view3D1 = new C.View3D();
            C.RotateX rotateX1 = new C.RotateX(){ Val = 30 };
            C.Perspective perspective1 = new C.Perspective(){ Val = 30 };

            view3D1.Append(rotateX1);
            view3D1.Append(perspective1);

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout1 = new C.Layout();

            C.Pie3DChart pie3DChart1 = new C.Pie3DChart();
            C.VaryColors varyColors1 = new C.VaryColors(){ Val = true };

            C.PieChartSeries pieChartSeries1 = new C.PieChartSeries();
            C.Index index1 = new C.Index(){ Val = (UInt32Value)0U };
            C.Order order1 = new C.Order(){ Val = (UInt32Value)0U };

            C.SeriesText seriesText1 = new C.SeriesText();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula1 = new C.Formula();
            formula1.Text = "Sheet1!$B$1";

            C.StringCache stringCache1 = new C.StringCache();
            C.PointCount pointCount1 = new C.PointCount(){ Val = (UInt32Value)1U };

            C.StringPoint stringPoint1 = new C.StringPoint(){ Index = (UInt32Value)0U };
            C.NumericValue numericValue1 = new C.NumericValue();
            numericValue1.Text = "Sales";

            stringPoint1.Append(numericValue1);

            stringCache1.Append(pointCount1);
            stringCache1.Append(stringPoint1);

            stringReference1.Append(formula1);
            stringReference1.Append(stringCache1);

            seriesText1.Append(stringReference1);

            C.CategoryAxisData categoryAxisData1 = new C.CategoryAxisData();

            C.StringReference stringReference2 = new C.StringReference();
            C.Formula formula2 = new C.Formula();
            formula2.Text = "Sheet1!$A$2:$A$5";

            C.StringCache stringCache2 = new C.StringCache();
            C.PointCount pointCount2 = new C.PointCount(){ Val = (UInt32Value)4U };

            C.StringPoint stringPoint2 = new C.StringPoint(){ Index = (UInt32Value)0U };
            C.NumericValue numericValue2 = new C.NumericValue();
            numericValue2.Text = "1st Qtr";

            stringPoint2.Append(numericValue2);

            C.StringPoint stringPoint3 = new C.StringPoint(){ Index = (UInt32Value)1U };
            C.NumericValue numericValue3 = new C.NumericValue();
            numericValue3.Text = "2nd Qtr";

            stringPoint3.Append(numericValue3);

            C.StringPoint stringPoint4 = new C.StringPoint(){ Index = (UInt32Value)2U };
            C.NumericValue numericValue4 = new C.NumericValue();
            numericValue4.Text = "3rd Qtr";

            stringPoint4.Append(numericValue4);

            C.StringPoint stringPoint5 = new C.StringPoint(){ Index = (UInt32Value)3U };
            C.NumericValue numericValue5 = new C.NumericValue();
            numericValue5.Text = "4th Qtr";

            stringPoint5.Append(numericValue5);

            stringCache2.Append(pointCount2);
            stringCache2.Append(stringPoint2);
            stringCache2.Append(stringPoint3);
            stringCache2.Append(stringPoint4);
            stringCache2.Append(stringPoint5);

            stringReference2.Append(formula2);
            stringReference2.Append(stringCache2);

            categoryAxisData1.Append(stringReference2);

            C.Values values1 = new C.Values();

            C.NumberReference numberReference1 = new C.NumberReference();
            C.Formula formula3 = new C.Formula();
            formula3.Text = "Sheet1!$B$2:$B$5";

            C.NumberingCache numberingCache1 = new C.NumberingCache();
            C.FormatCode formatCode1 = new C.FormatCode();
            formatCode1.Text = "General";
            C.PointCount pointCount3 = new C.PointCount(){ Val = (UInt32Value)4U };

            C.NumericPoint numericPoint1 = new C.NumericPoint(){ Index = (UInt32Value)0U };
            C.NumericValue numericValue6 = new C.NumericValue();
            numericValue6.Text = "8.2000000000000011";

            numericPoint1.Append(numericValue6);

            C.NumericPoint numericPoint2 = new C.NumericPoint(){ Index = (UInt32Value)1U };
            C.NumericValue numericValue7 = new C.NumericValue();
            numericValue7.Text = "3.2";

            numericPoint2.Append(numericValue7);

            C.NumericPoint numericPoint3 = new C.NumericPoint(){ Index = (UInt32Value)2U };
            C.NumericValue numericValue8 = new C.NumericValue();
            numericValue8.Text = "1.4";

            numericPoint3.Append(numericValue8);

            C.NumericPoint numericPoint4 = new C.NumericPoint(){ Index = (UInt32Value)3U };
            C.NumericValue numericValue9 = new C.NumericValue();
            numericValue9.Text = "1.2";

            numericPoint4.Append(numericValue9);

            numberingCache1.Append(formatCode1);
            numberingCache1.Append(pointCount3);
            numberingCache1.Append(numericPoint1);
            numberingCache1.Append(numericPoint2);
            numberingCache1.Append(numericPoint3);
            numberingCache1.Append(numericPoint4);

            numberReference1.Append(formula3);
            numberReference1.Append(numberingCache1);

            values1.Append(numberReference1);

            pieChartSeries1.Append(index1);
            pieChartSeries1.Append(order1);
            pieChartSeries1.Append(seriesText1);
            pieChartSeries1.Append(categoryAxisData1);
            pieChartSeries1.Append(values1);

            pie3DChart1.Append(varyColors1);
            pie3DChart1.Append(pieChartSeries1);

            plotArea1.Append(layout1);
            plotArea1.Append(pie3DChart1);

            C.Legend legend1 = new C.Legend();
            C.LegendPosition legendPosition1 = new C.LegendPosition(){ Val = C.LegendPositionValues.Right };

            legend1.Append(legendPosition1);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly(){ Val = true };

            chart1.Append(title1);
            chart1.Append(view3D1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);

            //C.ExternalData externalData1 = new C.ExternalData(){ Id = "rId1" };

            //chartSpace1.Append(date19041);
            //chartSpace1.Append(editingLanguage1);
            //chartSpace1.Append(chart1);
            //chartSpace1.Append(externalData1);

            //part.ChartSpace = chartSpace1;
        }

        private static string embeddedPackagePart1Data = "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";

        private static System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

    }
}

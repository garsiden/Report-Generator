using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;
using RSMTenon.Data;

namespace RSMTenon.Graphing
{
    public class AllocationPieChart : PieGraph
    {
        public Chart GenerateChart(string title, List<AssetWeighting>model)
        {
            Chart chart1 = new Chart();
            Title title1 = GenerateTitle(title);

            View3D view3D1 = new View3D();
            RotateX rotateX1 = new RotateX() { Val = 30 };
            Perspective perspective1 = new Perspective() { Val = 30 };

            view3D1.Append(rotateX1);
            view3D1.Append(perspective1);

            PlotArea plotArea1 = new PlotArea();
            Layout layout1 = new Layout();

            Pie3DChart pie3DChart1 = new Pie3DChart();
            VaryColors varyColors1 = new VaryColors() { Val = true };

            PieChartSeries pieChartSeries1 = new PieChartSeries();
            Index index1 = new Index() { Val = (UInt32Value)0U };
            Order order1 = new Order() { Val = (UInt32Value)0U };

            // c:tx series text
            SeriesText seriesText1 = GenerateSeriesText(title); // new SeriesText();

            // c:cat category axis data
            //var categoryData = model.OrderByDescending(m => m.Weighting).Select(n => n.AssetClass).ToArray<string>();
            var categoryData = model.OrderBy(m => m.AssetClass).Select(n => n.AssetClass).ToArray<string>();

            CategoryAxisData categoryAxisData1 = GenerateCategoryAxisData(categoryData, "A");

            // c:val values
            var valuesData = model.OrderBy(m => m.AssetClass).Select(n => n.Weighting.Value).ToArray<double>();
//            var valuesData = model.OrderByDescending(m => m.Weighting).Select(n => n.Weighting.Value).ToArray<double>();
            Values values1 = GenerateValues("General", valuesData, "B");

            pieChartSeries1.Append(index1);
            pieChartSeries1.Append(order1);
            pieChartSeries1.Append(seriesText1);
            pieChartSeries1.Append(categoryAxisData1);
            pieChartSeries1.Append(values1);

            pie3DChart1.Append(varyColors1);
            pie3DChart1.Append(pieChartSeries1);

            plotArea1.Append(layout1);
            plotArea1.Append(pie3DChart1);

            Legend legend1 = GenerateLegend(LegendPositionValues.Right);
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };

            chart1.Append(title1);
            chart1.Append(view3D1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);

            return chart1;
        }

        protected override Legend GenerateLegend(LegendPositionValues position)
        {
            Legend legend1 = new Legend();
            LegendPosition legendPosition1 = new LegendPosition() { Val = position };

            Layout layout1 = new Layout();

            // Manual layout to accomodate all Asset Classes
            ManualLayout manualLayout1 = new ManualLayout();
            LeftMode leftMode1 = new LeftMode() { Val = LayoutModeValues.Edge };
            TopMode topMode1 = new TopMode() { Val = LayoutModeValues.Edge };
            Left left1 = new Left() { Val = 0.74094630872483225D };
            Top top1 = new Top() { Val = 0.13422099673202617D };
            Width width1 = new Width() { Val = 0.24484787472035793D };
            Height height1 = new Height() { Val = 0.84387581699346426D };

            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);

            TextProperties textProperties1 = new TextProperties();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties1 = new A.DefaultRunProperties() { Language = "en-GB" };

            paragraphProperties1.Append(defaultRunProperties1);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties1);
            textProperties1.Append(listStyle1);
            textProperties1.Append(paragraph1);

            legend1.Append(legendPosition1);
            legend1.Append(layout1);
            legend1.Append(textProperties1);

            return legend1;
        }

        protected override SeriesText GenerateSeriesText(string seriesName)
        {
            SeriesText seriesText1 = new SeriesText();

            StringReference stringReference1 = new StringReference();
            Formula formula1 = new Formula();
            formula1.Text = "Sheet1!$B$1";

            StringCache stringCache1 = new StringCache();
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)1U };

            StringPoint stringPoint1 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = seriesName;

            stringPoint1.Append(numericValue1);

            stringCache1.Append(pointCount1);
            stringCache1.Append(stringPoint1);

            stringReference1.Append(formula1);
            stringReference1.Append(stringCache1);

            seriesText1.Append(stringReference1);
            return seriesText1;
        }

        protected CategoryAxisData GenerateCategoryAxisData(string[] data, string column)
        {
            CategoryAxisData categoryAxisData1 = new CategoryAxisData();

            StringReference stringReference1 = new StringReference();
            Formula formula1 = new Formula();
            uint len = (uint)data.Length;
            formula1.Text = formulaColumn(column, 2, len);
            //"Sheet1!$A$2:$A$5";

            StringCache stringCache1 = new StringCache();
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)len };
            stringCache1.Append(pointCount1);

            for (int i = 0; i < len; i++) {
                StringPoint stringPoint1 = GenerateStringPoint((uint)i);
                NumericValue numericValue1 = new NumericValue() { Text = data[i] };
                stringPoint1.Append(numericValue1);
                stringCache1.Append(stringPoint1);
            }

            stringReference1.Append(formula1);
            stringReference1.Append(stringCache1);

            categoryAxisData1.Append(stringReference1);
            return categoryAxisData1;
        }


        protected StringPoint GenerateStringPoint(uint index)
        {
            StringPoint stringPoint1 = new StringPoint() { Index = (UInt32Value)index };

            return stringPoint1;
        }

        public Values GenerateValues(string format, double[] data, string column)
        {
            uint len = (uint)data.Length;
            Values values1 = new Values();

            NumberReference numberReference1 = new NumberReference();
            Formula formula1 = new Formula();
            formula1.Text = formulaColumn(column, 2, len);
            //"Sheet1!$B$2:$B$5";

            NumberingCache numberingCache1 = new NumberingCache();
            FormatCode formatCode1 = new FormatCode();
            formatCode1.Text = format; //"General;
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)len };
            numberingCache1.Append(formatCode1);
            numberingCache1.Append(pointCount1);

            for (uint i = 0; i < len; i++) {
                NumericPoint numericPoint1 = GenerateNumericPoint((UInt32Value)i, data[i].ToString());
                numberingCache1.Append(numericPoint1);
            }

            numberReference1.Append(formula1);
            numberReference1.Append(numberingCache1);

            values1.Append(numberReference1);
            return values1;
        }

        protected string formulaColumn(string column, int start, uint length)
        {
            string col = column.ToUpper();
            string form = String.Format("Sheet1!${0}${1}:${2}${3}", col, start, col, length + 1);

            return form;
        }

    }
}

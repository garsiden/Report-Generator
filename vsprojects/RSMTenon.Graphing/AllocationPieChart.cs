﻿using System;
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

            Pie3DChart pie3DChart1 = new Pie3DChart();
            VaryColors varyColors1 = new VaryColors() { Val = true };

            PieChartSeries pieChartSeries1 = new PieChartSeries();
            Index index1 = new Index() { Val = (UInt32Value)0U };
            Order order1 = new Order() { Val = (UInt32Value)0U };

            SeriesText seriesText1 = new SeriesText();
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = title + "Series";

            seriesText1.Append(numericValue1);

            CategoryAxisData categoryAxisData1 = new CategoryAxisData();

            StringLiteral stringLiteral1 = new StringLiteral();
            NumberLiteral numberLiteral1 = new NumberLiteral();

            FormatCode formatCode1 = new FormatCode();
            formatCode1.Text = "General";
            numberLiteral1.Append(formatCode1);

            UInt32 numPoints = (UInt32)model.Count();
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)numPoints };
            stringLiteral1.Append(pointCount1);

            PointCount pointCount2 = new PointCount() { Val = (UInt32Value)numPoints };
            numberLiteral1.Append(pointCount2);

            uint i = 0U;

            foreach (var alloc in model.OrderByDescending(m => m.Weighting)) {
                StringPoint stringPoint1 = GenerateStringPoint(i, alloc.AssetClass);
                stringLiteral1.Append(stringPoint1);
                NumericPoint numericPoint1 = GenerateNumericPoint(i++, alloc.Weighting.ToString());
                numberLiteral1.Append(numericPoint1);
            }

            categoryAxisData1.Append(stringLiteral1);

            Values values1 = new Values();
            values1.Append(numberLiteral1);

            pieChartSeries1.Append(index1);
            pieChartSeries1.Append(order1);
            pieChartSeries1.Append(seriesText1);
            pieChartSeries1.Append(categoryAxisData1);
            pieChartSeries1.Append(values1);

            pie3DChart1.Append(varyColors1);
            pie3DChart1.Append(pieChartSeries1);

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
    }
}

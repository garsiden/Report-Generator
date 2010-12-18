﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;

namespace RSMTenon.Graphing
{


    public class StressTestBarChart : BarGraph
    {
        private string pointFormat = "0.00%";

        public void AddBarChartSeries(Chart barChart, BarGraphSeries series)
        {
            BarChartSeries barChartSeries = GenerateBarChartSeries(series.Name, series.PointNames, series.Values, series.ColourHex, pointFormat);
            barChart.PlotArea.ChildElements.First<BarChart>().Append(barChartSeries);
        }

        public Chart GenerateChartCrash(string title)
        {
            float[] global = { -0.15650000000000006F, -0.28730000000000011F, -0.2782F };
            float[] bonds = { 6.9600000000000023E-2F, 8.2300000000000012E-2F, 9.3000000000000055E-2F };
            float[] strategy = { 7.9000000000000042E-3F, 1.7100000000000001E-2F, 0.10600000000000002F };

            string[] pointNames = {"Russian Debt Crisis & LTCM Collapse Aug 98 - Sep 98",
                                      "Technology Bubble Bursting Apr 00 - Sep 01",
                                        "Economic Slowdown May 02 - Sep 02"};
            Chart chart1 = new Chart();
            Title title1 = GenerateTitle("Stress Test - Market Crashes");
            PlotArea plotArea1 = new PlotArea();

            Layout layout2 = GeneratePlotAreaLayout();

            BarChart barChart1 = new BarChart();
            BarDirection barDirection1 = new BarDirection() { Val = BarDirectionValues.Column };
            BarGrouping barGrouping1 = new BarGrouping() { Val = BarGroupingValues.Clustered };

            //BarChartSeries barChartSeries1 = GenerateBarChartSeries("Global Equities", 1U, 0U, pointNames, global, "808080", "0.00%");
            //BarChartSeries barChartSeries2 = GenerateBarChartSeries("UK Government Bonds", 2U, 1U, pointNames, bonds, "C0C0C0", "0.00%");
            //BarChartSeries barChartSeries3 = GenerateBarChartSeries("Defensive Strategy", 3U, 2U, pointNames, strategy, "0066CC", "0.00%");

            AxisId axisId1 = new AxisId() { Val = (UInt32Value)92179456U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)92463872U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            //barChart1.Append(barChartSeries1);
            //barChart1.Append(barChartSeries2);
            //barChart1.Append(barChartSeries3);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            CategoryAxis categoryAxis1 = GenerateCategoryAxis(axisId1, AxisPositionValues.Bottom, "General", axisId2);

            ValueAxis valueAxis1 = GenerateValueAxis(axisId2, AxisPositionValues.Left, "0%", axisId1);

            ShapeProperties shapeProperties1 = new ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline8 = new A.Outline() { Width = 25400 };
            A.NoFill noFill4 = new A.NoFill();

            outline8.Append(noFill4);

            shapeProperties1.Append(noFill3);
            shapeProperties1.Append(outline8);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            Legend legend1 = GenerateLegend(LegendPositionValues.Right);
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };
            DisplayBlanksAs displayBlanksAs1 = new DisplayBlanksAs() { Val = DisplayBlanksAsValues.Gap };

            chart1.Append(title1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);

            return chart1;
        }

        protected override Layout GeneratePlotAreaLayout()
        {
            Layout layout1 = new Layout();

            ManualLayout manualLayout1 = new ManualLayout();
            LayoutTarget layoutTarget1 = new LayoutTarget() { Val = LayoutTargetValues.Inner };
            LeftMode leftMode1 = new LeftMode() { Val = LayoutModeValues.Edge };
            TopMode topMode1 = new TopMode() { Val = LayoutModeValues.Edge };
            Left left1 = new Left() { Val = 0.1045898583146905D };
            Top top1 = new Top() { Val = 0.12075204248366019D };
            Width width1 = new Width() { Val = 0.85622038461448768D };
            Height height1 = new Height() { Val = 0.54928769841269842D };

            manualLayout1.Append(layoutTarget1);
            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);

            return layout1;
        }

        protected Layout GenerateLegendLayout()
        {
            Layout layout1 = new Layout();

            ManualLayout manualLayout1 = new ManualLayout();
            LeftMode leftMode1 = new LeftMode() { Val = LayoutModeValues.Edge };
            TopMode topMode1 = new TopMode() { Val = LayoutModeValues.Edge };
            Left left1 = new Left() { Val = 6.6235864297253616E-2D };
            Top top1 = new Top() { Val = 0.89666946631671063D };
            Width width1 = new Width() { Val = 0.86429793206384342D };
            Height height1 = new Height() { Val = 8.0000349956255767E-2D };

            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);
            return layout1;
        }

        protected override Legend GenerateLegend(LegendPositionValues position)
        {
            Legend legend1 = new Legend();
            LegendPosition legendPosition1 = new LegendPosition() { Val = position };

            Layout layout3 = GenerateLegendLayout();

            ChartShapeProperties chartShapeProperties8 = new ChartShapeProperties();

            A.SolidFill solidFill10 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill10.Append(rgbColorModelHex10);

            A.Outline outline9 = new A.Outline() { Width = 3175 };

            A.SolidFill solidFill11 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill11.Append(rgbColorModelHex11);
            A.PresetDash presetDash7 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline9.Append(solidFill11);
            outline9.Append(presetDash7);

            chartShapeProperties8.Append(solidFill10);
            chartShapeProperties8.Append(outline9);

            legend1.Append(legendPosition1);
            legend1.Append(layout3);
            legend1.Append(chartShapeProperties8);

            return legend1;
        }

        protected CategoryAxis GenerateCategoryAxis(AxisId axisId, AxisPositionValues axisPosition, string formatCode, AxisId crossingAxisId)
        {
            CategoryAxis categoryAxis1 = new CategoryAxis();
            AxisId axisId3 = new AxisId() { Val = axisId.Val };

            Scaling scaling1 = new Scaling();
            Orientation orientation1 = new Orientation() { Val = OrientationValues.MinMax };

            scaling1.Append(orientation1);
            AxisPosition axisPosition1 = new AxisPosition() { Val = axisPosition };
            NumberingFormat numberingFormat1 = new NumberingFormat() { FormatCode = formatCode, SourceLinked = true };
            TickLabelPosition tickLabelPosition1 = new TickLabelPosition() { Val = TickLabelPositionValues.Low };

            ChartShapeProperties chartShapeProperties5 = GenerateChartShapeProperties(3175);

            TextProperties textProperties1 = new TextProperties();
            A.BodyProperties bodyProperties2 = new A.BodyProperties() { Rotation = 0, Vertical = A.TextVerticalValues.Horizontal };
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties();

            paragraphProperties2.Append(defaultRunProperties2);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = DEFAULT_LANG };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties2);
            textProperties1.Append(listStyle2);
            textProperties1.Append(paragraph2);

            CrossingAxis crossingAxis1 = new CrossingAxis() { Val = crossingAxisId.Val };
            Crosses crosses1 = new Crosses() { Val = CrossesValues.AutoZero };
            AutoLabeled autoLabeled1 = new AutoLabeled() { Val = true };
            LabelAlignment labelAlignment1 = new LabelAlignment() { Val = LabelAlignmentValues.Center };
            LabelOffset labelOffset1 = new LabelOffset() { Val = (UInt16Value)100U };
            TickLabelSkip tickLabelSkip1 = new TickLabelSkip() { Val = 1 };
            TickMarkSkip tickMarkSkip1 = new TickMarkSkip() { Val = 1 };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(chartShapeProperties5);
            categoryAxis1.Append(textProperties1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(tickLabelSkip1);
            categoryAxis1.Append(tickMarkSkip1);

            return categoryAxis1;
        }

        public  Chart GenerateChartRise(string title)
        {
            //double[] global = { 0.7686D, 0.0555D };
            //double[] bonds = { 0.1527D, 0.5255D };
            //double[] strategy = { 0.2898D, 0.5510D };
            //double[] current = { 0.3805D, 0.4397D };

            //string[] pointNames = { "Bull Market Mar 03 - Mar 06", "10 Year Returns Dec 99 - Dec 09" };

            Chart chart1 = new Chart();
            Title title1 = GenerateTitle(title);
            PlotArea plotArea1 = new PlotArea();

            Layout layout2 = GeneratePlotAreaLayout();

            BarChart barChart1 = new BarChart();
            BarDirection barDirection1 = new BarDirection() { Val = BarDirectionValues.Column };
            BarGrouping barGrouping1 = new BarGrouping() { Val = BarGroupingValues.Clustered };

            //BarChartSeries barChartSeries1 = GenerateBarChartSeries("Global Equities", pointNames, global, "808080", "0.00%");
            //BarChartSeries barChartSeries2 = GenerateBarChartSeries("UK Government Bonds", pointNames, bonds, "C0C0C0", "0.00%");
            //BarChartSeries barChartSeries3 = GenerateBarChartSeries("Defensive Strategy", pointNames, strategy, "0066CC", "0.00%");
            //BarChartSeries barChartSeries4 = GenerateBarChartSeries("Current Portfolio", pointNames, current, "99CC00", "0.00%");

            AxisId axisId1 = new AxisId() { Val = (UInt32Value)92179456U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)92463872U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            //barChart1.Append(barChartSeries4);
            //barChart1.Append(barChartSeries1);
            //barChart1.Append(barChartSeries2);
            //barChart1.Append(barChartSeries3);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            CategoryAxis categoryAxis1 = GenerateCategoryAxis(axisId1, AxisPositionValues.Bottom, "General", axisId2);
            ValueAxis valueAxis1 = GenerateValueAxis(axisId2, AxisPositionValues.Left, "0%", axisId1);

            ShapeProperties shapeProperties1 = new ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline8 = new A.Outline() { Width = 25400 };
            A.NoFill noFill4 = new A.NoFill();

            outline8.Append(noFill4);

            shapeProperties1.Append(noFill3);
            shapeProperties1.Append(outline8);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            Legend legend1 = GenerateLegend(LegendPositionValues.Right);
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };
            DisplayBlanksAs displayBlanksAs1 = new DisplayBlanksAs() { Val = DisplayBlanksAsValues.Gap };

            chart1.Append(title1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);

            return chart1;
        }
    }
}

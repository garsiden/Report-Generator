using System;
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

        public new  static long Cy
        {
            get
            {
                return (long)(7.00 * EMUS_PER_CENTIMETRE);
            }
        }

        public void AddBarChartSeries(Chart chart, BarGraphSeries series)
        {
            BarChartSeries barChartSeries = GenerateBarChartSeries(series.Name, series.PointNames, series.Values, series.ColourHex, pointFormat);
            BarChart barChart = chart.PlotArea.ChildElements.First<BarChart>();
            var bcs = chart.PlotArea.Descendants<BarChartSeries>().LastOrDefault();

            if (bcs == null)
            {
                var grp = barChart.ChildElements.First<BarGrouping>();
                barChart.InsertAfter<BarChartSeries>(barChartSeries, grp);
            } else
            {
                barChart.InsertAfter<BarChartSeries>(barChartSeries, bcs);
            }
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

        public Chart GenerateChart(string title)
        {
            Chart chart1 = new Chart();
            Title title1 = GenerateTitle(title);
            PlotArea plotArea1 = new PlotArea();

            Layout layout2 = GeneratePlotAreaLayout();

            BarChart barChart1 = new BarChart();
            BarDirection barDirection1 = new BarDirection() { Val = BarDirectionValues.Column };
            BarGrouping barGrouping1 = new BarGrouping() { Val = BarGroupingValues.Clustered };

            AxisId axisId1 = new AxisId() { Val = (UInt32Value)92179456U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)92463872U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
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

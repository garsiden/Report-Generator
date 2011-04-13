using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;

using RSMTenon.Data;

namespace RSMTenon.Graphing
{
    public class AllocationComparisonBarChart : BarGraph
    {
        private readonly string colourHex = "0066CC";
        private readonly string seriesName = "Under/Over Allocation";

        public AllocationComparisonBarChart()
        {
            valueFormat = "General";
            valueAxisFormat = "0%";
            categoryAxisFormat = "General";
            categoryName = "Asset Class";
        }

        public Chart GenerateChart(string title, List<AssetWeighting> data)
        {
            string[] pointNames = data.Select(p => p.AssetClass).ToArray();
            double[] values = data.Select(v => v.Weighting ?? 0).ToArray();

            Chart chart1 = new Chart();

            Title title1 = GenerateTitle(title, 1200);
            PlotArea plotArea1 = new PlotArea();

            Layout layout2 = GeneratePlotAreaLayout();
            LayoutTarget layoutTarget1 = new LayoutTarget() { Val = LayoutTargetValues.Inner };
            LeftMode leftMode2 = new LeftMode() { Val = LayoutModeValues.Edge };
            TopMode topMode2 = new TopMode() { Val = LayoutModeValues.Edge };
            Left left2 = new Left() { Val = 0.10397108399455671D };
            Top top2 = new Top() { Val = 0.13928263342174291D };
            Width width1 = new Width() { Val = 0.88016518444392156D };
            Height height1 = new Height() { Val = 0.59349350997070749D };

            BarChart barChart1 = new BarChart();
            BarDirection barDirection1 = new BarDirection() { Val = BarDirectionValues.Column };
            BarGrouping barGrouping1 = new BarGrouping() { Val = BarGroupingValues.Clustered };
            BarChartSeries barChartSeries1 = GenerateBarChartSeries(seriesName, pointNames, values, colourHex, valueFormat);

            AxisId axisId1 = new AxisId() { Val = (UInt32Value)97045504U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)97055488U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(barChartSeries1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            ValueAxis valueAxis1 = GenerateValueAxis(axisId2, AxisPositionValues.Left, valueAxisFormat, axisId1);
            CategoryAxis categoryAxis1 = GenerateCategoryAxis(axisId1, AxisPositionValues.Bottom, categoryAxisFormat, axisId2);

            ShapeProperties shapeProperties1 = new ShapeProperties();
            A::NoFill noFill3 = new A::NoFill();

            A::Outline outline6 = new A::Outline() { Width = 25400 };
            A::NoFill noFill4 = new A::NoFill();

            outline6.Append(noFill4);

            shapeProperties1.Append(noFill3);
            shapeProperties1.Append(outline6);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };
            DisplayBlanksAs displayBlanksAs1 = new DisplayBlanksAs() { Val = DisplayBlanksAsValues.Gap };

            chart1.Append(title1);
            chart1.Append(plotArea1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);

            return chart1;
        }

        protected CategoryAxis GenerateCategoryAxis(AxisId axisId, AxisPositionValues axisPosition, string formatCode, AxisId crossingAxisId)
        {
            CategoryAxis categoryAxis1 = new CategoryAxis();
            AxisId axisId1 = new AxisId() { Val = axisId.Val };

            Scaling scaling1 = new Scaling();
            Orientation orientation1 = new Orientation() { Val = OrientationValues.MinMax };

            scaling1.Append(orientation1);
            AxisPosition axisPosition1 = new AxisPosition() { Val = AxisPositionValues.Bottom };
            NumberingFormat numberingFormat1 = new NumberingFormat() { FormatCode = formatCode, SourceLinked = true };
            TickLabelPosition tickLabelPosition1 = new TickLabelPosition() { Val = TickLabelPositionValues.Low };

            ChartShapeProperties chartShapeProperties1 = GenerateChartShapeProperties(3175);

            TextProperties textProperties1 = new TextProperties();
            A::BodyProperties bodyProperties1 = new A::BodyProperties() { Rotation = -1800000, Vertical = A::TextVerticalValues.Horizontal };
            A::ListStyle listStyle1 = new A::ListStyle();

            A::Paragraph paragraph1 = new A::Paragraph();

            A::ParagraphProperties paragraphProperties1 = new A::ParagraphProperties();

            A::DefaultRunProperties defaultRunProperties1 = new A::DefaultRunProperties() { Language = DEFAULT_LANG, FontSize = 1000, Bold = false, Italic = false, Underline = A::TextUnderlineValues.None, Strike = A::TextStrikeValues.NoStrike, Baseline = 0 };

            A::SolidFill solidFill2 = new A::SolidFill();
            A::RgbColorModelHex rgbColorModelHex2 = new A::RgbColorModelHex() { Val = "000000" };

            solidFill2.Append(rgbColorModelHex2);

            defaultRunProperties1.Append(solidFill2);

            paragraphProperties1.Append(defaultRunProperties1);
            A::EndParagraphRunProperties endParagraphRunProperties1 = new A::EndParagraphRunProperties() { Language = DEFAULT_LANG };

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties1);
            textProperties1.Append(listStyle1);
            textProperties1.Append(paragraph1);
            CrossingAxis crossingAxis1 = new CrossingAxis() { Val = (UInt32Value)crossingAxisId.Val };
            Crosses crosses1 = new Crosses() { Val = CrossesValues.AutoZero };
            AutoLabeled autoLabeled1 = new AutoLabeled() { Val = true };
            LabelAlignment labelAlignment1 = new LabelAlignment() { Val = LabelAlignmentValues.Center };
            LabelOffset labelOffset1 = new LabelOffset() { Val = (UInt16Value)100U };
            TickLabelSkip tickLabelSkip1 = new TickLabelSkip() { Val = 1 };
            TickMarkSkip tickMarkSkip1 = new TickMarkSkip() { Val = 1 };

            categoryAxis1.Append(axisId1);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(chartShapeProperties1);
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

        protected override BarChartSeries GenerateBarChartSeries(string seriesName, string[] pointNames, double[] vals, string colourHex, string valueFormat)
        {
            var barChartSeries1 = base.GenerateBarChartSeries(seriesName, pointNames, vals, colourHex, valueFormat);

            // remove unused series text
            SeriesText seriesText = barChartSeries1.Descendants<SeriesText>().FirstOrDefault();

            if (seriesText != null)
                seriesText.Remove();

            return barChartSeries1;
        }
    }
}

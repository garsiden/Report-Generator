using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;

namespace RSMTenon.Graphing
{
    public class AllocationBarChart  : BarGraph
    {

        public Chart GenerateChart(string title)
        {
            float[] vals = { 0.11768250185249102F, -6.1324324147486134E-2F, 4.7902237547416453E-2F, 3.846389646333704E-2F, 0.0F, -6.8212317552127408E-2F, -0.2F, -0.15F, 0.27375221930876331F, 1.7357865276057976E-3F, 0.0F, 0.0F };
            string[] pointNames = {"Cash",
            "UK Gov Bonds",
            "UK Corp Bonds",
            "UK HY Bonds",
            "Global Bonds",
            "Real Estate",
            "Hedge Funds",
            "L/S Equity",
            "UK Equity",
            "Global Equity",
            "Private Equity",
            "Commodities"};

            Chart chart1 = new Chart();

            Title title1 = GenerateTitle(title, 1200);
            PlotArea plotArea1 = new PlotArea();
            Layout layout2 = GeneratePlotAreaLayout();

            BarChart barChart1 = new BarChart();
            BarDirection barDirection1 = new BarDirection() { Val = BarDirectionValues.Column };
            BarGrouping barGrouping1 = new BarGrouping() { Val = BarGroupingValues.Clustered };
            BarChartSeries barChartSeries1 = GenerateBarChartSeries(null, 0U,0U,pointNames,vals, "0066CC",null);

            AxisId axisId1 = new AxisId() { Val = (UInt32Value)97045504U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)97055488U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(barChartSeries1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            ValueAxis valueAxis1 = GenerateValueAxis(axisId2, AxisPositionValues.Left, "0%", axisId1);
            CategoryAxis categoryAxis1 = GenerateCategoryAxis(axisId1, AxisPositionValues.Bottom, "General", axisId2);

            ShapeProperties shapeProperties1 = new ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline6 = new A.Outline();// { Width = 25400 };
            A.NoFill noFill4 = new A.NoFill();

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

        public BarChartSeries GenerateBarChartSeries(string seriesName, uint index, uint order, string[] pointNames, float[] vals, string colourHex, string valueFormat)
        {
            BarChartSeries barChartSeries1 = new BarChartSeries();
            Index index1 = new Index() { Val = (UInt32Value)index };
            Order order1 = new Order() { Val = (UInt32Value)order };

            ChartShapeProperties chartShapeProperties2 = GenerateChartShapeProperties(colourHex, 12700);
            CategoryAxisData categoryAxisData1 = GenerateCategoryAxisData(pointNames);
            Values values1 = GenerateValues("0.00%", vals);

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(chartShapeProperties2);
            barChartSeries1.Append(categoryAxisData1);
            barChartSeries1.Append(values1);

            return barChartSeries1;
        }

        public CategoryAxis GenerateCategoryAxis(AxisId axisId, AxisPositionValues axisPosition, string formatCode, AxisId crossingAxisId)
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
            A.BodyProperties bodyProperties1 = new A.BodyProperties() { Rotation = -1800000, Vertical = A.TextVerticalValues.Horizontal };
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties1 = new A.DefaultRunProperties() { Language = DEFAULT_LANG, FontSize = 1000, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

            A.SolidFill solidFill2 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill2.Append(rgbColorModelHex2);

            defaultRunProperties1.Append(solidFill2);

            paragraphProperties1.Append(defaultRunProperties1);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = DEFAULT_LANG };

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
        public ValueAxis GenerateValueAxis(AxisId axisId, AxisPositionValues position, string formatCode, AxisId crossingAxisId)
        {
            ValueAxis valueAxis1 = new ValueAxis();
            AxisId axisId4 = new AxisId() { Val = (UInt32Value)axisId.Val };

            Scaling scaling2 = new Scaling();
            Orientation orientation2 = new Orientation() { Val = OrientationValues.MinMax };

            scaling2.Append(orientation2);
            AxisPosition axisPosition2 = new AxisPosition() { Val = position };

            MajorGridlines majorGridlines1 = new MajorGridlines();

            ChartShapeProperties chartShapeProperties4 = GenerateChartShapeProperties(3175);
            majorGridlines1.Append(chartShapeProperties4);
            NumberingFormat numberingFormat2 = new NumberingFormat() { FormatCode = formatCode, SourceLinked = false };
            TickLabelPosition tickLabelPosition2 = new TickLabelPosition() { Val = TickLabelPositionValues.NextTo };

            ChartShapeProperties chartShapeProperties5 = new ChartShapeProperties();

            A.Outline outline5 = new A.Outline();// { Width = 3175 };

            A.SolidFill solidFill7 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill7.Append(rgbColorModelHex7);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline5.Append(solidFill7);
            outline5.Append(presetDash4);

            chartShapeProperties5.Append(outline5);

            TextProperties textProperties2 = new TextProperties();
            A.BodyProperties bodyProperties3 = new A.BodyProperties() { Rotation = 0, Vertical = A.TextVerticalValues.Horizontal };
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties() { Language = DEFAULT_LANG, FontSize = 1000, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

            A.SolidFill solidFill8 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill8.Append(rgbColorModelHex8);

            paragraphProperties3.Append(defaultRunProperties3);
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(endParagraphRunProperties2);

            textProperties2.Append(bodyProperties3);
            textProperties2.Append(listStyle3);
            textProperties2.Append(paragraph3);
            CrossingAxis crossingAxis2 = new CrossingAxis() { Val = (UInt32Value)crossingAxisId.Val };
            Crosses crosses2 = new Crosses() { Val = CrossesValues.AutoZero };
            CrossBetween crossBetween1 = new CrossBetween() { Val = CrossBetweenValues.Between };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(majorGridlines1);
            valueAxis1.Append(numberingFormat2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(chartShapeProperties5);
            valueAxis1.Append(textProperties2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);

            return valueAxis1;

        }

    }
}

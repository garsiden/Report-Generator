using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;

namespace RSMTenon.Graphing
{
    public class BarGraphSeries
    {
        public string Name { get; set; }
        public string[] PointNames { get; set; }
        public double[] Values { get; set; }
        public string Format { get;  set; }
        public string ColourHex { get; set; }
    }

    public abstract class BarGraph : Graph
    {
        protected ChartShapeProperties GenerateChartShapeProperties(string colourHex, int width)
        {
            ChartShapeProperties chartShapeProperties2 = new ChartShapeProperties();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = colourHex };
            solidFill1.Append(rgbColorModelHex1);

            A.Outline outline2 = new A.Outline() { Width = width };
            A.SolidFill solidFill2 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill2.Append(rgbColorModelHex2);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline2.Append(solidFill2);
            outline2.Append(presetDash1);

            chartShapeProperties2.Append(solidFill1);
            chartShapeProperties2.Append(outline2);

            return chartShapeProperties2;
        }

        protected BarChartSeries GenerateBarChartSeries(string seriesName, string[] pointNames, double[] vals, string colourHex, string valueFormat)
        {
            BarChartSeries barChartSeries1 = new BarChartSeries();
            Index index1 = new Index() { Val = (UInt32Value)index };
            Order order1 = new Order() { Val = (UInt32Value)order };

            SeriesText seriesText = GenerateSeriesText(seriesName);
            ChartShapeProperties chartShapeProperties2 = GenerateChartShapeProperties(colourHex, 12700);
            CategoryAxisData categoryAxisData1 = GenerateCategoryAxisData(pointNames);
            Values values1 = GenerateValues(valueFormat, vals);

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(seriesText);
            barChartSeries1.Append(chartShapeProperties2);
            barChartSeries1.Append(categoryAxisData1);
            barChartSeries1.Append(values1);

            this.index++;
            this.order++;

            return barChartSeries1;
        }

        protected ValueAxis GenerateValueAxis(AxisId axisId, AxisPositionValues position, string formatCode, AxisId crossingAxisId)
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

            ChartShapeProperties chartShapeProperties5 = GenerateChartShapeProperties(3175);

            TextProperties textProperties2 = new TextProperties();
            A.BodyProperties bodyProperties3 = new A.BodyProperties() { Rotation = 0, Vertical = A.TextVerticalValues.Horizontal };
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties() { Language = DEFAULT_LANG };

            A.SolidFill solidFill8 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill8.Append(rgbColorModelHex8);

            paragraphProperties3.Append(defaultRunProperties3);
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = DEFAULT_LANG };

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

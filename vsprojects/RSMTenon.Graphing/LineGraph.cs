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
    public abstract class LineGraph : Graph
    {
        protected string axisFormat;
        protected string valueFormat;
        protected string dateAxisFormat;
        protected string valueAxisFormat;
        protected string[] colours = { "C0C0C0", "808080", "0066CC", "98CC00" };

        public void AddLineChartSeries(Chart lineChart, List<ReturnData> data, string seriesName)
        {
            LineChartSeries lineChartSeries = GenerateLineChartSeries(seriesName, data);
            lineChart.PlotArea.ChildElements.First<LineChart>().Append(lineChartSeries);
        }

        protected LineChartSeries GenerateLineChartSeries(string seriesName, List<ReturnData> data)
        {
            uint numPoints = (uint)data.Count();
            string colourHex = colours[order];

            // c:ser (LineChartSeries)
            LineChartSeries lineChartSeries1 = new LineChartSeries();
            Index index1 = new Index() { Val = (UInt32Value)index };
            Order order1 = new Order() { Val = (UInt32Value)order };

            // c:tx (SeriesText)
            SeriesText seriesText1 = GenerateSeriesText(seriesName);

            // c:spPr (ChartShapeProperties)
            ChartShapeProperties chartShapeProperties1 = GenerateChartShapeProperties(colourHex);

            // c:marker (Marker)
            Marker marker1 = new Marker();
            Symbol symbol1 = new Symbol() { Val = MarkerStyleValues.None };
            marker1.Append(symbol1);

            // c:cat (CategoryAxisData)
            CategoryAxisData categoryAxisData1 = GenerateCategoryAxisData(axisFormat, data);

            // c:val (Values)
            Values values1 = GenerateValues(valueFormat, data);

            lineChartSeries1.Append(index1);
            lineChartSeries1.Append(order1);
            lineChartSeries1.Append(seriesText1);
            lineChartSeries1.Append(chartShapeProperties1);
            lineChartSeries1.Append(marker1);
            lineChartSeries1.Append(categoryAxisData1);
            lineChartSeries1.Append(values1);

            this.index++;
            this.order++;

            return lineChartSeries1;
        }

        protected DateAxis GenerateDateAxis(AxisId axisId, AxisPositionValues axisPosition, AxisId crossingAxisId, TickLabelPositionValues tickLabelPosition)
        {
            DateAxis dateAxis1 = new DateAxis();
            AxisId axisId3 = new AxisId() { Val = axisId.Val };

            Scaling scaling1 = new Scaling();
            Orientation orientation1 = new Orientation() { Val = OrientationValues.MinMax };

            scaling1.Append(orientation1);
            AxisPosition axisPosition1 = new AxisPosition() { Val = AxisPositionValues.Bottom };
            NumberingFormat numberingFormat1 = new NumberingFormat() { FormatCode = dateAxisFormat, SourceLinked = true };
            MajorTickMark majorTickMark1 = new MajorTickMark() { Val = TickMarkValues.None };
            TickLabelPosition tickLabelPosition1 = new TickLabelPosition() { Val = tickLabelPosition };

            TextProperties textProperties1 = new TextProperties();
            A.BodyProperties bodyProperties2 = new A.BodyProperties() { Rotation = -5400000, Vertical = A.TextVerticalValues.Horizontal };
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties() { Language = DEFAULT_LANG };

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
            LabelOffset labelOffset1 = new LabelOffset() { Val = (UInt16Value)100U };

            dateAxis1.Append(axisId3);
            dateAxis1.Append(scaling1);
            dateAxis1.Append(axisPosition1);
            dateAxis1.Append(numberingFormat1);
            dateAxis1.Append(majorTickMark1);
            dateAxis1.Append(tickLabelPosition1);
            dateAxis1.Append(textProperties1);
            dateAxis1.Append(crossingAxis1);
            dateAxis1.Append(crosses1);
            dateAxis1.Append(autoLabeled1);
            dateAxis1.Append(labelOffset1);

            return dateAxis1;
        }

        protected ValueAxis GenerateValueAxis(AxisId axisId, AxisPositionValues position, AxisId crossingAxisId)
        {
            ValueAxis valueAxis1 = new ValueAxis();
            AxisId axisId4 = new AxisId() { Val = axisId.Val };

            Scaling scaling2 = new Scaling();
            Orientation orientation2 = new Orientation() { Val = OrientationValues.MinMax };

            scaling2.Append(orientation2);
            AxisPosition axisPosition2 = new AxisPosition() { Val = position };
            MajorGridlines majorGridlines1 = new MajorGridlines();
            NumberingFormat numberingFormat2 = new NumberingFormat() { FormatCode = valueAxisFormat, SourceLinked = false };
            MajorTickMark majorTickMark2 = new MajorTickMark() { Val = TickMarkValues.None };
            TickLabelPosition tickLabelPosition2 = new TickLabelPosition() { Val = TickLabelPositionValues.NextTo };

            ChartShapeProperties chartShapeProperties4 = GenerateChartShapeProperties(9525);

            TextProperties textProperties2 = new TextProperties();
            A.BodyProperties bodyProperties3 = new A.BodyProperties();
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties() { Language = DEFAULT_LANG };

            paragraphProperties3.Append(defaultRunProperties3);
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = DEFAULT_LANG };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(endParagraphRunProperties2);

            textProperties2.Append(bodyProperties3);
            textProperties2.Append(listStyle3);
            textProperties2.Append(paragraph3);
            CrossingAxis crossingAxis2 = new CrossingAxis() { Val = crossingAxisId.Val };
            Crosses crosses2 = new Crosses() { Val = CrossesValues.AutoZero };
            CrossBetween crossBetween1 = new CrossBetween() { Val = CrossBetweenValues.Between };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(majorGridlines1);
            valueAxis1.Append(numberingFormat2);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(chartShapeProperties4);
            valueAxis1.Append(textProperties2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);

            return valueAxis1;
        }
    }
}

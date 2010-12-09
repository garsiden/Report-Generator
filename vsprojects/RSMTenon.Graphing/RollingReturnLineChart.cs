using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;

namespace RSMTenon.Graphing
{
    public class RollingReturnLineChart : LineGraph
    {
        public Chart GenerateChart(string title, Dictionary<string, decimal> data)
        {
            int[] dates = { 35795, 35825, 35853, 35885, 35915, 35944, 35976, 36007, 36038, 36068 };
            float[] vals1 = { 0.13391881756578716F, 0.14135951028946941F, 0.13040516889825624F, 0.16154388221487725F, 0.15511067322277042F, 0.14587027748661438F, 0.13265738026184651F, 0.12819829560599968F, 0.15818540512656329F, 0.15639454380333218F };
            float[] vals2 = { 0.18240488665525859F, 0.13719636964421847F, 0.2023129960674655F, 0.25505764011250265F, 0.22470727329039256F, 0.1792123093706717F, 0.14553573405056669F, 9.9659954050917349E-2F, -1.7888654716325683E-2F, -6.6674939924710824E-2F};
            float[] vals3 = { 0.15224447832611041F, 0.13359515161058083F, 0.14098577339425858F, 0.17317018077742041F, 0.16727699126207812F, 0.15223997821064741F, 0.14086755873197027F, 0.11834256487264866F, 8.4171018307112461E-2F, 5.9370181760978025E-2F};
            UInt32 numPoints = (UInt32)dates.Length;

            // c:chart (Chart)
            Chart chart1 = new Chart();

            // c:title (Title)
            Title title1 = GenerateTitle("1y Rolling Return", 1200);

            // c:plotArea (PlotArea)
            PlotArea plotArea1 = new PlotArea();
            // c:layout (Layout)
            Layout layout2 = new Layout();

            // c:lineChart (LineChart)
            LineChart lineChart1 = new LineChart();
            // c:grouping (Grouping)
            Grouping grouping1 = new Grouping() { Val = GroupingValues.Standard };

            // c:ser (LineChartSeries)
            LineChartSeries lineChartSeries1 = GenerateLineChartSeries("UK Gov Bonds", 1U, 0U, dates, vals1, "C0C0C0", "mmm\\-yy", "0.0%");
            LineChartSeries lineChartSeries2 = GenerateLineChartSeries("Global Equity", 2U, 1U, dates, vals2, "808080", "mmm\\-yy", "0.0%");
            LineChartSeries lineChartSeries3 = GenerateLineChartSeries("Defensive Strategy", 3U, 2U, dates, vals3, "0066CC", "mmm\\-yy", "0.0%");

            // c:marker (Marker)
            ShowMarker showMarker1 = new ShowMarker() { Val = true };
            // c:axId (AxisId)
            AxisId axisId1 = new AxisId() { Val = (UInt32Value)54573696U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)54657408U };

            lineChart1.Append(grouping1);
            lineChart1.Append(lineChartSeries1);
            lineChart1.Append(lineChartSeries2);
            lineChart1.Append(lineChartSeries3);
            lineChart1.Append(showMarker1);
            lineChart1.Append(axisId1);
            lineChart1.Append(axisId2);

            // c:dateAx (DateAxis)
            DateAxis dateAxis1 = GenerateDateAxis(axisId1, AxisPositionValues.Bottom, "mmm\\-yy", axisId2);

            // c:valAx (ValueAxis)
            ValueAxis valueAxis1 = GenerateValueAxis(axisId2, AxisPositionValues.Left, "0%", axisId1);

            // c:plotArea (PlotArea)
            plotArea1.Append(layout2);
            plotArea1.Append(lineChart1);
            plotArea1.Append(dateAxis1);
            plotArea1.Append(valueAxis1);

            // c:legend (Legend)
            Legend legend1 = GenerateLegend(LegendPositionValues.Bottom);

            // c:plotVisOnly (PlotVisibleOnly)
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };

            chart1.Append(title1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);

            return chart1;
        }

        public LineChartSeries GenerateLineChartSeries(string seriesName, uint index, uint order, int[] dates, float[] vals, string colourHex, string axisFormat, string valueFormat)
        {
            uint numPoints = (uint)dates.Length;

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
            CategoryAxisData categoryAxisData1 = GenerateCategoryAxisData(axisFormat, dates);

            // c:val (Values)
            Values values1 = GenerateValues(valueFormat, vals);

            lineChartSeries1.Append(index1);
            lineChartSeries1.Append(order1);
            lineChartSeries1.Append(seriesText1);
            lineChartSeries1.Append(chartShapeProperties1);
            lineChartSeries1.Append(marker1);
            lineChartSeries1.Append(categoryAxisData1);
            lineChartSeries1.Append(values1);

            return lineChartSeries1;
        }

 
        public ChartShapeProperties GenerateChartShapeProperties(string colourHex)
        {
            ChartShapeProperties chartShapeProperties1 = new ChartShapeProperties();

            A.Outline outline1 = new A.Outline();
            A.SolidFill solidFill1 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = colourHex };
            solidFill1.Append(rgbColorModelHex1);
            outline1.Append(solidFill1);
            chartShapeProperties1.Append(outline1);

            return chartShapeProperties1;
        }



        public DateAxis GenerateDateAxis(AxisId axisId, AxisPositionValues axisPosition, string formatCode, AxisId crossingAxisId)
        {
            DateAxis dateAxis1 = new DateAxis();
            AxisId axisId3 = new AxisId() { Val = axisId.Val };

            Scaling scaling1 = new Scaling();
            Orientation orientation1 = new Orientation() { Val = OrientationValues.MinMax };

            scaling1.Append(orientation1);
            AxisPosition axisPosition1 = new AxisPosition() { Val = AxisPositionValues.Bottom };
            NumberingFormat numberingFormat1 = new NumberingFormat() { FormatCode = formatCode, SourceLinked = true };
            MajorTickMark majorTickMark1 = new MajorTickMark() { Val = TickMarkValues.None };
            TickLabelPosition tickLabelPosition1 = new TickLabelPosition() { Val = TickLabelPositionValues.Low };

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

        public ValueAxis GenerateValueAxis(AxisId axisId, AxisPositionValues position, string formatCode, AxisId crossingAxisId)
        {
            ValueAxis valueAxis1 = new ValueAxis();
            AxisId axisId4 = new AxisId() { Val = axisId.Val };

            Scaling scaling2 = new Scaling();
            Orientation orientation2 = new Orientation() { Val = OrientationValues.MinMax };

            scaling2.Append(orientation2);
            AxisPosition axisPosition2 = new AxisPosition() { Val = position };
            MajorGridlines majorGridlines1 = new MajorGridlines();
            NumberingFormat numberingFormat2 = new NumberingFormat() { FormatCode = formatCode, SourceLinked = false };
            MajorTickMark majorTickMark2 = new MajorTickMark() { Val = TickMarkValues.None };
            TickLabelPosition tickLabelPosition2 = new TickLabelPosition() { Val = TickLabelPositionValues.NextTo };

            ChartShapeProperties chartShapeProperties4 = new ChartShapeProperties();

            A.Outline outline4 = new A.Outline() { Width = 9525 };
            A.NoFill noFill1 = new A.NoFill();

            outline4.Append(noFill1);

            chartShapeProperties4.Append(outline4);

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


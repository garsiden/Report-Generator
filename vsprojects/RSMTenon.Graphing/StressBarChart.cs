using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;
namespace RSMTenon.Graphing
{
    public class StressBarChart : BarGraph
    {
        public Chart GenerateChart(string title)
        {
            Chart chart1 = new Chart();
            Title title1 = GenerateTitle("Stress Test - Market Crashes");
            PlotArea plotArea1 = new PlotArea();

            Layout layout2 = GenerateLayout();

            BarChart barChart1 = new BarChart();
            BarDirection barDirection1 = new BarDirection() { Val = BarDirectionValues.Column };
            BarGrouping barGrouping1 = new BarGrouping() { Val = BarGroupingValues.Clustered };

            BarChartSeries barChartSeries1 = new BarChartSeries();
            Index index1 = new Index() { Val = (UInt32Value)1U };
            Order order1 = new Order() { Val = (UInt32Value)0U };

            SeriesText seriesText1 = new SeriesText();

            StringReference stringReference1 = new StringReference();

            StringCache stringCache1 = new StringCache();
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)1U };

            StringPoint stringPoint1 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = "Global Equities";

            stringPoint1.Append(numericValue1);

            stringCache1.Append(pointCount1);
            stringCache1.Append(stringPoint1);

            stringReference1.Append(stringCache1);

            seriesText1.Append(stringReference1);

            ChartShapeProperties chartShapeProperties2 = new ChartShapeProperties();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "808080" };

            solidFill1.Append(rgbColorModelHex1);

            A.Outline outline2 = new A.Outline() { Width = 12700 };

            A.SolidFill solidFill2 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill2.Append(rgbColorModelHex2);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline2.Append(solidFill2);
            outline2.Append(presetDash1);

            chartShapeProperties2.Append(solidFill1);
            chartShapeProperties2.Append(outline2);

            CategoryAxisData categoryAxisData1 = new CategoryAxisData();

            StringReference stringReference2 = new StringReference();

            StringCache stringCache2 = new StringCache();
            PointCount pointCount2 = new PointCount() { Val = (UInt32Value)3U };

            StringPoint stringPoint2 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue2 = new NumericValue();
            numericValue2.Text = "Russian Debt Crisis & LTCM Collapse Aug 98 - Sep 98";

            stringPoint2.Append(numericValue2);

            StringPoint stringPoint3 = new StringPoint() { Index = (UInt32Value)1U };
            NumericValue numericValue3 = new NumericValue();
            numericValue3.Text = "Technology Bubble Bursting Apr 00 - Sep 01";

            stringPoint3.Append(numericValue3);

            StringPoint stringPoint4 = new StringPoint() { Index = (UInt32Value)2U };
            NumericValue numericValue4 = new NumericValue();
            numericValue4.Text = "Economic Slowdown May 02 - Sep 02";

            stringPoint4.Append(numericValue4);

            stringCache2.Append(pointCount2);
            stringCache2.Append(stringPoint2);
            stringCache2.Append(stringPoint3);
            stringCache2.Append(stringPoint4);

            stringReference2.Append(stringCache2);

            categoryAxisData1.Append(stringReference2);

            Values values1 = new Values();

            NumberReference numberReference1 = new NumberReference();

            NumberingCache numberingCache1 = new NumberingCache();
            FormatCode formatCode1 = new FormatCode();
            formatCode1.Text = "0.00%";
            PointCount pointCount3 = new PointCount() { Val = (UInt32Value)3U };

            NumericPoint numericPoint1 = new NumericPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue5 = new NumericValue();
            numericValue5.Text = "-0.15650000000000006";

            numericPoint1.Append(numericValue5);

            NumericPoint numericPoint2 = new NumericPoint() { Index = (UInt32Value)1U };
            NumericValue numericValue6 = new NumericValue();
            numericValue6.Text = "-0.28730000000000011";

            numericPoint2.Append(numericValue6);

            NumericPoint numericPoint3 = new NumericPoint() { Index = (UInt32Value)2U };
            NumericValue numericValue7 = new NumericValue();
            numericValue7.Text = "-0.2782";

            numericPoint3.Append(numericValue7);

            numberingCache1.Append(formatCode1);
            numberingCache1.Append(pointCount3);
            numberingCache1.Append(numericPoint1);
            numberingCache1.Append(numericPoint2);
            numberingCache1.Append(numericPoint3);

            numberReference1.Append(numberingCache1);

            values1.Append(numberReference1);

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(seriesText1);
            barChartSeries1.Append(chartShapeProperties2);
            barChartSeries1.Append(categoryAxisData1);
            barChartSeries1.Append(values1);

            BarChartSeries barChartSeries2 = new BarChartSeries();
            Index index2 = new Index() { Val = (UInt32Value)2U };
            Order order2 = new Order() { Val = (UInt32Value)1U };

            SeriesText seriesText2 = new SeriesText();

            StringReference stringReference3 = new StringReference();

            StringCache stringCache3 = new StringCache();
            PointCount pointCount4 = new PointCount() { Val = (UInt32Value)1U };

            StringPoint stringPoint5 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue8 = new NumericValue();
            numericValue8.Text = "UK Government Bonds";

            stringPoint5.Append(numericValue8);

            stringCache3.Append(pointCount4);
            stringCache3.Append(stringPoint5);

            stringReference3.Append(stringCache3);

            seriesText2.Append(stringReference3);

            ChartShapeProperties chartShapeProperties3 = new ChartShapeProperties();

            A.SolidFill solidFill3 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "C0C0C0" };

            solidFill3.Append(rgbColorModelHex3);

            A.Outline outline3 = new A.Outline() { Width = 12700 };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill4.Append(rgbColorModelHex4);
            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline3.Append(solidFill4);
            outline3.Append(presetDash2);

            chartShapeProperties3.Append(solidFill3);
            chartShapeProperties3.Append(outline3);

            CategoryAxisData categoryAxisData2 = new CategoryAxisData();

            StringReference stringReference4 = new StringReference();

            StringCache stringCache4 = new StringCache();
            PointCount pointCount5 = new PointCount() { Val = (UInt32Value)3U };

            StringPoint stringPoint6 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue9 = new NumericValue();
            numericValue9.Text = "Russian Debt Crisis & LTCM Collapse Aug 98 - Sep 98";

            stringPoint6.Append(numericValue9);

            StringPoint stringPoint7 = new StringPoint() { Index = (UInt32Value)1U };
            NumericValue numericValue10 = new NumericValue();
            numericValue10.Text = "Technology Bubble Bursting Apr 00 - Sep 01";

            stringPoint7.Append(numericValue10);

            StringPoint stringPoint8 = new StringPoint() { Index = (UInt32Value)2U };
            NumericValue numericValue11 = new NumericValue();
            numericValue11.Text = "Economic Slowdown May 02 - Sep 02";

            stringPoint8.Append(numericValue11);

            stringCache4.Append(pointCount5);
            stringCache4.Append(stringPoint6);
            stringCache4.Append(stringPoint7);
            stringCache4.Append(stringPoint8);

            stringReference4.Append(stringCache4);

            categoryAxisData2.Append(stringReference4);

            Values values2 = new Values();

            NumberReference numberReference2 = new NumberReference();

            NumberingCache numberingCache2 = new NumberingCache();
            FormatCode formatCode2 = new FormatCode();
            formatCode2.Text = "0.00%";
            PointCount pointCount6 = new PointCount() { Val = (UInt32Value)3U };

            NumericPoint numericPoint4 = new NumericPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue12 = new NumericValue();
            numericValue12.Text = "6.9600000000000023E-2";

            numericPoint4.Append(numericValue12);

            NumericPoint numericPoint5 = new NumericPoint() { Index = (UInt32Value)1U };
            NumericValue numericValue13 = new NumericValue();
            numericValue13.Text = "8.2300000000000012E-2";

            numericPoint5.Append(numericValue13);

            NumericPoint numericPoint6 = new NumericPoint() { Index = (UInt32Value)2U };
            NumericValue numericValue14 = new NumericValue();
            numericValue14.Text = "9.3000000000000055E-2";

            numericPoint6.Append(numericValue14);

            numberingCache2.Append(formatCode2);
            numberingCache2.Append(pointCount6);
            numberingCache2.Append(numericPoint4);
            numberingCache2.Append(numericPoint5);
            numberingCache2.Append(numericPoint6);

            numberReference2.Append(numberingCache2);

            values2.Append(numberReference2);

            barChartSeries2.Append(index2);
            barChartSeries2.Append(order2);
            barChartSeries2.Append(seriesText2);
            barChartSeries2.Append(chartShapeProperties3);
            barChartSeries2.Append(categoryAxisData2);
            barChartSeries2.Append(values2);

            BarChartSeries barChartSeries3 = new BarChartSeries();
            Index index3 = new Index() { Val = (UInt32Value)3U };
            Order order3 = new Order() { Val = (UInt32Value)2U };

            SeriesText seriesText3 = new SeriesText();

            StringReference stringReference5 = new StringReference();

            StringCache stringCache5 = new StringCache();
            PointCount pointCount7 = new PointCount() { Val = (UInt32Value)1U };

            StringPoint stringPoint9 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue15 = new NumericValue();
            numericValue15.Text = "Defensive Strategy";

            stringPoint9.Append(numericValue15);

            stringCache5.Append(pointCount7);
            stringCache5.Append(stringPoint9);

            stringReference5.Append(stringCache5);

            seriesText3.Append(stringReference5);

            ChartShapeProperties chartShapeProperties4 = new ChartShapeProperties();

            A.SolidFill solidFill5 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "0066CC" };

            solidFill5.Append(rgbColorModelHex5);

            A.Outline outline4 = new A.Outline() { Width = 12700 };

            A.SolidFill solidFill6 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill6.Append(rgbColorModelHex6);
            A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline4.Append(solidFill6);
            outline4.Append(presetDash3);

            chartShapeProperties4.Append(solidFill5);
            chartShapeProperties4.Append(outline4);

            CategoryAxisData categoryAxisData3 = new CategoryAxisData();

            StringReference stringReference6 = new StringReference();

            StringCache stringCache6 = new StringCache();
            PointCount pointCount8 = new PointCount() { Val = (UInt32Value)3U };

            StringPoint stringPoint10 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue16 = new NumericValue();
            numericValue16.Text = "Russian Debt Crisis & LTCM Collapse Aug 98 - Sep 98";

            stringPoint10.Append(numericValue16);

            StringPoint stringPoint11 = new StringPoint() { Index = (UInt32Value)1U };
            NumericValue numericValue17 = new NumericValue();
            numericValue17.Text = "Technology Bubble Bursting Apr 00 - Sep 01";

            stringPoint11.Append(numericValue17);

            StringPoint stringPoint12 = new StringPoint() { Index = (UInt32Value)2U };
            NumericValue numericValue18 = new NumericValue();
            numericValue18.Text = "Economic Slowdown May 02 - Sep 02";

            stringPoint12.Append(numericValue18);

            stringCache6.Append(pointCount8);
            stringCache6.Append(stringPoint10);
            stringCache6.Append(stringPoint11);
            stringCache6.Append(stringPoint12);

            stringReference6.Append(stringCache6);

            categoryAxisData3.Append(stringReference6);

            Values values3 = new Values();

            NumberReference numberReference3 = new NumberReference();

            NumberingCache numberingCache3 = new NumberingCache();
            FormatCode formatCode3 = new FormatCode();
            formatCode3.Text = "0.00%";
            PointCount pointCount9 = new PointCount() { Val = (UInt32Value)3U };

            NumericPoint numericPoint7 = new NumericPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue19 = new NumericValue();
            numericValue19.Text = "7.9000000000000042E-3";

            numericPoint7.Append(numericValue19);

            NumericPoint numericPoint8 = new NumericPoint() { Index = (UInt32Value)1U };
            NumericValue numericValue20 = new NumericValue();
            numericValue20.Text = "1.7100000000000001E-2";

            numericPoint8.Append(numericValue20);

            NumericPoint numericPoint9 = new NumericPoint() { Index = (UInt32Value)2U };
            NumericValue numericValue21 = new NumericValue();
            numericValue21.Text = "0.10600000000000002";

            numericPoint9.Append(numericValue21);

            numberingCache3.Append(formatCode3);
            numberingCache3.Append(pointCount9);
            numberingCache3.Append(numericPoint7);
            numberingCache3.Append(numericPoint8);
            numberingCache3.Append(numericPoint9);

            numberReference3.Append(numberingCache3);

            values3.Append(numberReference3);

            barChartSeries3.Append(index3);
            barChartSeries3.Append(order3);
            barChartSeries3.Append(seriesText3);
            barChartSeries3.Append(chartShapeProperties4);
            barChartSeries3.Append(categoryAxisData3);
            barChartSeries3.Append(values3);
            AxisId axisId1 = new AxisId() { Val = (UInt32Value)92179456U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)92463872U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(barChartSeries1);
            barChart1.Append(barChartSeries2);
            barChart1.Append(barChartSeries3);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            CategoryAxis categoryAxis1 = new CategoryAxis();
            AxisId axisId3 = new AxisId() { Val = (UInt32Value)92179456U };

            Scaling scaling1 = new Scaling();
            Orientation orientation1 = new Orientation() { Val = OrientationValues.MinMax };

            scaling1.Append(orientation1);
            AxisPosition axisPosition1 = new AxisPosition() { Val = AxisPositionValues.Bottom };
            NumberingFormat numberingFormat1 = new NumberingFormat() { FormatCode = "General", SourceLinked = true };
            TickLabelPosition tickLabelPosition1 = new TickLabelPosition() { Val = TickLabelPositionValues.Low };

            ChartShapeProperties chartShapeProperties5 = new ChartShapeProperties();

            A.Outline outline5 = new A.Outline() { Width = 3175 };

            A.SolidFill solidFill7 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill7.Append(rgbColorModelHex7);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline5.Append(solidFill7);
            outline5.Append(presetDash4);

            chartShapeProperties5.Append(outline5);

            TextProperties textProperties1 = new TextProperties();
            A.BodyProperties bodyProperties2 = new A.BodyProperties() { Rotation = 0, Vertical = A.TextVerticalValues.Horizontal };
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties();

            paragraphProperties2.Append(defaultRunProperties2);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties2);
            textProperties1.Append(listStyle2);
            textProperties1.Append(paragraph2);
            CrossingAxis crossingAxis1 = new CrossingAxis() { Val = (UInt32Value)92463872U };
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

            ValueAxis valueAxis1 = new ValueAxis();
            AxisId axisId4 = new AxisId() { Val = (UInt32Value)92463872U };

            Scaling scaling2 = new Scaling();
            Orientation orientation2 = new Orientation() { Val = OrientationValues.MinMax };

            scaling2.Append(orientation2);
            AxisPosition axisPosition2 = new AxisPosition() { Val = AxisPositionValues.Left };

            MajorGridlines majorGridlines1 = new MajorGridlines();

            ChartShapeProperties chartShapeProperties6 = new ChartShapeProperties();

            A.Outline outline6 = new A.Outline() { Width = 3175 };

            A.SolidFill solidFill8 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill8.Append(rgbColorModelHex8);
            A.PresetDash presetDash5 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline6.Append(solidFill8);
            outline6.Append(presetDash5);

            chartShapeProperties6.Append(outline6);

            majorGridlines1.Append(chartShapeProperties6);
            NumberingFormat numberingFormat2 = new NumberingFormat() { FormatCode = "0%", SourceLinked = false };
            TickLabelPosition tickLabelPosition2 = new TickLabelPosition() { Val = TickLabelPositionValues.NextTo };

            ChartShapeProperties chartShapeProperties7 = new ChartShapeProperties();

            A.Outline outline7 = new A.Outline() { Width = 3175 };

            A.SolidFill solidFill9 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill9.Append(rgbColorModelHex9);
            A.PresetDash presetDash6 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline7.Append(solidFill9);
            outline7.Append(presetDash6);

            chartShapeProperties7.Append(outline7);

            TextProperties textProperties2 = new TextProperties();
            A.BodyProperties bodyProperties3 = new A.BodyProperties() { Rotation = 0, Vertical = A.TextVerticalValues.Horizontal };
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties();

            paragraphProperties3.Append(defaultRunProperties3);
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(endParagraphRunProperties2);

            textProperties2.Append(bodyProperties3);
            textProperties2.Append(listStyle3);
            textProperties2.Append(paragraph3);
            CrossingAxis crossingAxis2 = new CrossingAxis() { Val = (UInt32Value)92179456U };
            Crosses crosses2 = new Crosses() { Val = CrossesValues.AutoZero };
            CrossBetween crossBetween1 = new CrossBetween() { Val = CrossBetweenValues.Between };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(majorGridlines1);
            valueAxis1.Append(numberingFormat2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(chartShapeProperties7);
            valueAxis1.Append(textProperties2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);

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

            Legend legend1 = new Legend();
            LegendPosition legendPosition1 = new LegendPosition() { Val = LegendPositionValues.Right };

            Layout layout3 = new Layout();

            ManualLayout manualLayout3 = new ManualLayout();
            LeftMode leftMode3 = new LeftMode() { Val = LayoutModeValues.Edge };
            TopMode topMode3 = new TopMode() { Val = LayoutModeValues.Edge };
            Left left3 = new Left() { Val = 6.6235864297253616E-2D };
            Top top3 = new Top() { Val = 0.89666946631671063D };
            Width width2 = new Width() { Val = 0.8642979320638432D };
            Height height2 = new Height() { Val = 8.0000349956255767E-2D };

            manualLayout3.Append(leftMode3);
            manualLayout3.Append(topMode3);
            manualLayout3.Append(left3);
            manualLayout3.Append(top3);
            manualLayout3.Append(width2);
            manualLayout3.Append(height2);

            layout3.Append(manualLayout3);

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
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };
            DisplayBlanksAs displayBlanksAs1 = new DisplayBlanksAs() { Val = DisplayBlanksAsValues.Gap };

            chart1.Append(title1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            return chart1;
        }
        public Layout GenerateLayout()
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

    }
}

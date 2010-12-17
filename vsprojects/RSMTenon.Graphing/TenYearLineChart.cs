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
    public class TenYearLineChart  : LineGraph
    {
        // Creates an Chart instance and adds its children.
        public Chart GenerateChart()
        {
            Chart chart1 = new Chart();
            AutoTitleDeleted autoTitleDeleted1 = new AutoTitleDeleted(){ Val = true };

            PlotArea plotArea1 = new PlotArea();

            Layout layout1 = new Layout();

            ManualLayout manualLayout1 = new ManualLayout();
            LayoutTarget layoutTarget1 = new LayoutTarget(){ Val = LayoutTargetValues.Inner };
            LeftMode leftMode1 = new LeftMode(){ Val = LayoutModeValues.Edge };
            TopMode topMode1 = new TopMode(){ Val = LayoutModeValues.Edge };
            Left left1 = new Left(){ Val = 0.11843285214348206D };
            Top top1 = new Top(){ Val = 5.1298768155777226E-2D };
            Width width1 = new Width(){ Val = 0.85101159230096224D };
            Height height1 = new Height(){ Val = 0.58327109049811265D };

            manualLayout1.Append(layoutTarget1);
            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);

            LineChart lineChart1 = new LineChart();
            Grouping grouping1 = new Grouping(){ Val = GroupingValues.Standard };

            LineChartSeries lineChartSeries1 = new LineChartSeries();
            Index index1 = new Index(){ Val = (UInt32Value)1U };
            Order order1 = new Order(){ Val = (UInt32Value)0U };

            SeriesText seriesText1 = new SeriesText();

            StringReference stringReference1 = new StringReference();

            StringCache stringCache1 = new StringCache();
            PointCount pointCount1 = new PointCount(){ Val = (UInt32Value)1U };

            StringPoint stringPoint1 = new StringPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = "Global Equity";

            stringPoint1.Append(numericValue1);

            stringCache1.Append(pointCount1);
            stringCache1.Append(stringPoint1);

            stringReference1.Append(stringCache1);

            seriesText1.Append(stringReference1);

            ChartShapeProperties chartShapeProperties1 = new ChartShapeProperties();

            A.Outline outline1 = new A.Outline();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex(){ Val = "808080" };

            solidFill1.Append(rgbColorModelHex1);

            outline1.Append(solidFill1);

            chartShapeProperties1.Append(outline1);

            Marker marker1 = new Marker();
            Symbol symbol1 = new Symbol(){ Val = MarkerStyleValues.None };

            marker1.Append(symbol1);

            CategoryAxisData categoryAxisData1 = new CategoryAxisData();

            NumberReference numberReference1 = new NumberReference();
            Formula formula2 = new Formula();
            formula2.Text = "\'Client RR and DD\'!$FI$7:$FI$139";

            NumberingCache numberingCache1 = new NumberingCache();
            FormatCode formatCode1 = new FormatCode();
            formatCode1.Text = "mmm\\-yy";
            PointCount pointCount2 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint1 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue2 = new NumericValue();
            numericValue2.Text = "36373";

            numericPoint1.Append(numericValue2);

            NumericPoint numericPoint2 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue3 = new NumericValue();
            numericValue3.Text = "36404";

            numericPoint2.Append(numericValue3);

            numberingCache1.Append(formatCode1);
            numberingCache1.Append(pointCount2);
            numberingCache1.Append(numericPoint1);
            numberingCache1.Append(numericPoint2);
            numberReference1.Append(numberingCache1);

            categoryAxisData1.Append(numberReference1);

            Values values1 = new Values();

            NumberReference numberReference2 = new NumberReference();

            NumberingCache numberingCache2 = new NumberingCache();
            FormatCode formatCode2 = new FormatCode();
            formatCode2.Text = "General";
            PointCount pointCount3 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint132 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue133 = new NumericValue();
            numericValue133.Text = "0";

            numericPoint132.Append(numericValue133);

            NumericPoint numericPoint133 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue134 = new NumericValue();
            numericValue134.Text = "-3.2619220527717402E-2";

            numericPoint133.Append(numericValue134);

            NumericPoint numericPoint134 = new NumericPoint(){ Index = (UInt32Value)2U };
            NumericValue numericValue135 = new NumericValue();
            numericValue135.Text = "2.1665456305979089E-2";

            numericPoint134.Append(numericValue135);

            NumericPoint numericPoint135 = new NumericPoint(){ Index = (UInt32Value)3U };
            NumericValue numericValue136 = new NumericValue();
            numericValue136.Text = "8.4301621883321187E-2";

            numericPoint135.Append(numericValue136);


            numberingCache2.Append(formatCode2);
            numberingCache2.Append(pointCount3);
            numberingCache2.Append(numericPoint132);
            numberingCache2.Append(numericPoint133);
            numberingCache2.Append(numericPoint134);
            numberingCache2.Append(numericPoint135);
            numberReference2.Append(numberingCache2);

            values1.Append(numberReference2);

            lineChartSeries1.Append(index1);
            lineChartSeries1.Append(order1);
            lineChartSeries1.Append(seriesText1);
            lineChartSeries1.Append(chartShapeProperties1);
            lineChartSeries1.Append(marker1);
            lineChartSeries1.Append(categoryAxisData1);
            lineChartSeries1.Append(values1);

            LineChartSeries lineChartSeries2 = new LineChartSeries();
            Index index2 = new Index(){ Val = (UInt32Value)2U };
            Order order2 = new Order(){ Val = (UInt32Value)1U };

            SeriesText seriesText2 = new SeriesText();

            StringReference stringReference2 = new StringReference();

            StringCache stringCache2 = new StringCache();
            PointCount pointCount4 = new PointCount(){ Val = (UInt32Value)1U };

            StringPoint stringPoint2 = new StringPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue264 = new NumericValue();
            numericValue264.Text = "Cash";

            stringPoint2.Append(numericValue264);

            stringCache2.Append(pointCount4);
            stringCache2.Append(stringPoint2);

            stringReference2.Append(stringCache2);

            seriesText2.Append(stringReference2);

            ChartShapeProperties chartShapeProperties2 = new ChartShapeProperties();

            A.Outline outline2 = new A.Outline();

            A.SolidFill solidFill2 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill2.Append(schemeColor1);

            outline2.Append(solidFill2);

            chartShapeProperties2.Append(outline2);

            Marker marker2 = new Marker();
            Symbol symbol2 = new Symbol(){ Val = MarkerStyleValues.None };

            marker2.Append(symbol2);

            CategoryAxisData categoryAxisData2 = new CategoryAxisData();

            NumberReference numberReference3 = new NumberReference();

            NumberingCache numberingCache3 = new NumberingCache();
            FormatCode formatCode3 = new FormatCode();
            formatCode3.Text = "mmm\\-yy";
            PointCount pointCount5 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint263 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue265 = new NumericValue();
            numericValue265.Text = "36373";

            numericPoint263.Append(numericValue265);

            NumericPoint numericPoint264 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue266 = new NumericValue();
            numericValue266.Text = "36404";

            numericPoint264.Append(numericValue266);

            NumericPoint numericPoint265 = new NumericPoint(){ Index = (UInt32Value)2U };
            NumericValue numericValue267 = new NumericValue();
            numericValue267.Text = "36434";

            numericPoint265.Append(numericValue267);


            numberingCache3.Append(formatCode3);
            numberingCache3.Append(pointCount5);
            numberingCache3.Append(numericPoint263);
            numberingCache3.Append(numericPoint264);
            numberingCache3.Append(numericPoint265);
            numberReference3.Append(numberingCache3);

            categoryAxisData2.Append(numberReference3);

            Values values2 = new Values();

            NumberReference numberReference4 = new NumberReference();

            NumberingCache numberingCache4 = new NumberingCache();
            FormatCode formatCode4 = new FormatCode();
            formatCode4.Text = "General";
            PointCount pointCount6 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint394 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue396 = new NumericValue();
            numericValue396.Text = "0";

            numericPoint394.Append(numericValue396);

            NumericPoint numericPoint395 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue397 = new NumericValue();
            numericValue397.Text = "4.1409617172314481E-3";

            numericPoint395.Append(numericValue397);

            NumericPoint numericPoint396 = new NumericPoint(){ Index = (UInt32Value)2U };
            NumericValue numericValue398 = new NumericValue();
            numericValue398.Text = "8.1974140116622685E-3";

            numericPoint396.Append(numericValue398);

            NumericPoint numericPoint397 = new NumericPoint(){ Index = (UInt32Value)3U };
            NumericValue numericValue399 = new NumericValue();
            numericValue399.Text = "1.2760922842897034E-2";

            numericPoint397.Append(numericValue399);


            numberingCache4.Append(formatCode4);
            numberingCache4.Append(pointCount6);
            numberingCache4.Append(numericPoint394);
            numberingCache4.Append(numericPoint395);
            numberingCache4.Append(numericPoint396);
            numberingCache4.Append(numericPoint397);
            numberReference4.Append(numberingCache4);

            values2.Append(numberReference4);

            lineChartSeries2.Append(index2);
            lineChartSeries2.Append(order2);
            lineChartSeries2.Append(seriesText2);
            lineChartSeries2.Append(chartShapeProperties2);
            lineChartSeries2.Append(marker2);
            lineChartSeries2.Append(categoryAxisData2);
            lineChartSeries2.Append(values2);

            LineChartSeries lineChartSeries3 = new LineChartSeries();
            Index index3 = new Index(){ Val = (UInt32Value)3U };
            Order order3 = new Order(){ Val = (UInt32Value)2U };

            SeriesText seriesText3 = new SeriesText();

            StringReference stringReference3 = new StringReference();

            StringCache stringCache3 = new StringCache();
            PointCount pointCount7 = new PointCount(){ Val = (UInt32Value)1U };

            StringPoint stringPoint3 = new StringPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue527 = new NumericValue();
            numericValue527.Text = "IMA Cautious Managed";

            stringPoint3.Append(numericValue527);

            stringCache3.Append(pointCount7);
            stringCache3.Append(stringPoint3);

            stringReference3.Append(stringCache3);

            seriesText3.Append(stringReference3);

            ChartShapeProperties chartShapeProperties3 = new ChartShapeProperties();

            A.Outline outline3 = new A.Outline();

            A.SolidFill solidFill3 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex(){ Val = "C0C0C0" };

            solidFill3.Append(rgbColorModelHex2);

            outline3.Append(solidFill3);

            chartShapeProperties3.Append(outline3);

            Marker marker3 = new Marker();
            Symbol symbol3 = new Symbol(){ Val = MarkerStyleValues.None };

            marker3.Append(symbol3);

            CategoryAxisData categoryAxisData3 = new CategoryAxisData();

            NumberReference numberReference5 = new NumberReference();

            NumberingCache numberingCache5 = new NumberingCache();
            FormatCode formatCode5 = new FormatCode();
            formatCode5.Text = "mmm\\-yy";
            PointCount pointCount8 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint525 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue528 = new NumericValue();
            numericValue528.Text = "36373";

            numericPoint525.Append(numericValue528);

            NumericPoint numericPoint526 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue529 = new NumericValue();
            numericValue529.Text = "36404";

            numericPoint526.Append(numericValue529);

            NumericPoint numericPoint527 = new NumericPoint(){ Index = (UInt32Value)2U };
            NumericValue numericValue530 = new NumericValue();
            numericValue530.Text = "36434";

            numericPoint527.Append(numericValue530);


            numberingCache5.Append(formatCode5);
            numberingCache5.Append(pointCount8);
            numberingCache5.Append(numericPoint525);
            numberingCache5.Append(numericPoint526);
            numberingCache5.Append(numericPoint527);
            numberReference5.Append(numberingCache5);

            categoryAxisData3.Append(numberReference5);

            Values values3 = new Values();

            NumberReference numberReference6 = new NumberReference();

            NumberingCache numberingCache6 = new NumberingCache();
            FormatCode formatCode6 = new FormatCode();
            formatCode6.Text = "General";
            PointCount pointCount9 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint656 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue659 = new NumericValue();
            numericValue659.Text = "1.4504179460762582E-2";

            numericPoint656.Append(numericValue659);

            NumericPoint numericPoint657 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue660 = new NumericValue();
            numericValue660.Text = "-2.2444293500381188E-2";

            numericPoint657.Append(numericValue660);

            NumericPoint numericPoint658 = new NumericPoint(){ Index = (UInt32Value)2U };
            NumericValue numericValue661 = new NumericValue();
            numericValue661.Text = "-1.4296815877557097E-2";

            numericPoint658.Append(numericValue661);


            numberingCache6.Append(formatCode6);
            numberingCache6.Append(pointCount9);
            numberingCache6.Append(numericPoint656);
            numberingCache6.Append(numericPoint657);
            numberingCache6.Append(numericPoint658);
            numberReference6.Append(numberingCache6);

            values3.Append(numberReference6);

            lineChartSeries3.Append(index3);
            lineChartSeries3.Append(order3);
            lineChartSeries3.Append(seriesText3);
            lineChartSeries3.Append(chartShapeProperties3);
            lineChartSeries3.Append(marker3);
            lineChartSeries3.Append(categoryAxisData3);
            lineChartSeries3.Append(values3);

            LineChartSeries lineChartSeries4 = new LineChartSeries();
            Index index4 = new Index(){ Val = (UInt32Value)4U };
            Order order4 = new Order(){ Val = (UInt32Value)3U };

            SeriesText seriesText4 = new SeriesText();

            StringReference stringReference4 = new StringReference();

            StringCache stringCache4 = new StringCache();
            PointCount pointCount10 = new PointCount(){ Val = (UInt32Value)1U };

            StringPoint stringPoint4 = new StringPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue790 = new NumericValue();
            numericValue790.Text = "Defensive Strategy";

            stringPoint4.Append(numericValue790);

            stringCache4.Append(pointCount10);
            stringCache4.Append(stringPoint4);

            stringReference4.Append(stringCache4);

            seriesText4.Append(stringReference4);

            ChartShapeProperties chartShapeProperties4 = new ChartShapeProperties();

            A.Outline outline4 = new A.Outline();

            A.SolidFill solidFill4 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex(){ Val = "0066CC" };

            solidFill4.Append(rgbColorModelHex3);

            outline4.Append(solidFill4);

            chartShapeProperties4.Append(outline4);

            Marker marker4 = new Marker();
            Symbol symbol4 = new Symbol(){ Val = MarkerStyleValues.None };

            marker4.Append(symbol4);

            CategoryAxisData categoryAxisData4 = new CategoryAxisData();

            NumberReference numberReference7 = new NumberReference();

            NumberingCache numberingCache7 = new NumberingCache();
            FormatCode formatCode7 = new FormatCode();
            formatCode7.Text = "mmm\\-yy";
            PointCount pointCount11 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint787 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue791 = new NumericValue();
            numericValue791.Text = "36373";

            numericPoint787.Append(numericValue791);

            NumericPoint numericPoint788 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue792 = new NumericValue();
            numericValue792.Text = "36404";

            numericPoint788.Append(numericValue792);

            NumericPoint numericPoint789 = new NumericPoint(){ Index = (UInt32Value)2U };
            NumericValue numericValue793 = new NumericValue();
            numericValue793.Text = "36434";

            numericPoint789.Append(numericValue793);


            numberingCache7.Append(formatCode7);
            numberingCache7.Append(pointCount11);
            numberingCache7.Append(numericPoint787);
            numberingCache7.Append(numericPoint788);
            numberingCache7.Append(numericPoint789);
            numberReference7.Append(numberingCache7);

            categoryAxisData4.Append(numberReference7);

            Values values4 = new Values();

            NumberReference numberReference8 = new NumberReference();

            NumberingCache numberingCache8 = new NumberingCache();
            FormatCode formatCode8 = new FormatCode();
            formatCode8.Text = "General";
            PointCount pointCount12 = new PointCount(){ Val = (UInt32Value)133U };

            NumericPoint numericPoint918 = new NumericPoint(){ Index = (UInt32Value)0U };
            NumericValue numericValue922 = new NumericValue();
            numericValue922.Text = "0";

            numericPoint918.Append(numericValue922);

            NumericPoint numericPoint919 = new NumericPoint(){ Index = (UInt32Value)1U };
            NumericValue numericValue923 = new NumericValue();
            numericValue923.Text = "-4.9166963573347312E-3";

            numericPoint919.Append(numericValue923);

            NumericPoint numericPoint920 = new NumericPoint(){ Index = (UInt32Value)2U };
            NumericValue numericValue924 = new NumericValue();
            numericValue924.Text = "1.2539706466194782E-2";

            numericPoint920.Append(numericValue924);

            NumericPoint numericPoint921 = new NumericPoint(){ Index = (UInt32Value)3U };
            NumericValue numericValue925 = new NumericValue();
            numericValue925.Text = "4.3866759739776759E-2";

            numericPoint921.Append(numericValue925);


            numberingCache8.Append(formatCode8);
            numberingCache8.Append(pointCount12);
            numberingCache8.Append(numericPoint918);
            numberingCache8.Append(numericPoint919);
            numberingCache8.Append(numericPoint920);
            numberingCache8.Append(numericPoint921);
            numberReference8.Append(numberingCache8);

            values4.Append(numberReference8);

            lineChartSeries4.Append(index4);
            lineChartSeries4.Append(order4);
            lineChartSeries4.Append(seriesText4);
            lineChartSeries4.Append(chartShapeProperties4);
            lineChartSeries4.Append(marker4);
            lineChartSeries4.Append(categoryAxisData4);
            lineChartSeries4.Append(values4);
            ShowMarker showMarker1 = new ShowMarker(){ Val = true };
            AxisId axisId1 = new AxisId(){ Val = (UInt32Value)132034944U };
            AxisId axisId2 = new AxisId(){ Val = (UInt32Value)132036480U };

            lineChart1.Append(grouping1);
            lineChart1.Append(lineChartSeries1);
            lineChart1.Append(lineChartSeries2);
            lineChart1.Append(lineChartSeries3);
            lineChart1.Append(lineChartSeries4);
            lineChart1.Append(showMarker1);
            lineChart1.Append(axisId1);
            lineChart1.Append(axisId2);

            DateAxis dateAxis1 = new DateAxis();
            AxisId axisId3 = new AxisId(){ Val = (UInt32Value)132034944U };

            Scaling scaling1 = new Scaling();
            Orientation orientation1 = new Orientation(){ Val = OrientationValues.MinMax };

            scaling1.Append(orientation1);
            AxisPosition axisPosition1 = new AxisPosition(){ Val = AxisPositionValues.Bottom };
            NumberingFormat numberingFormat1 = new NumberingFormat(){ FormatCode = "mmm\\-yy", SourceLinked = true };
            MajorTickMark majorTickMark1 = new MajorTickMark(){ Val = TickMarkValues.None };
            TickLabelPosition tickLabelPosition1 = new TickLabelPosition(){ Val = TickLabelPositionValues.Low };

            TextProperties textProperties1 = new TextProperties();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties1 = new A.DefaultRunProperties(){ Language = "en-GB" };

            paragraphProperties1.Append(defaultRunProperties1);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties(){ Language = "en-US" };

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties1);
            textProperties1.Append(listStyle1);
            textProperties1.Append(paragraph1);
            CrossingAxis crossingAxis1 = new CrossingAxis(){ Val = (UInt32Value)132036480U };
            Crosses crosses1 = new Crosses(){ Val = CrossesValues.AutoZero };
            AutoLabeled autoLabeled1 = new AutoLabeled(){ Val = true };
            LabelOffset labelOffset1 = new LabelOffset(){ Val = (UInt16Value)100U };

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

            ValueAxis valueAxis1 = new ValueAxis();
            AxisId axisId4 = new AxisId(){ Val = (UInt32Value)132036480U };

            Scaling scaling2 = new Scaling();
            Orientation orientation2 = new Orientation(){ Val = OrientationValues.MinMax };

            scaling2.Append(orientation2);
            AxisPosition axisPosition2 = new AxisPosition(){ Val = AxisPositionValues.Left };
            MajorGridlines majorGridlines1 = new MajorGridlines();
            NumberingFormat numberingFormat2 = new NumberingFormat(){ FormatCode = "0%", SourceLinked = false };
            MajorTickMark majorTickMark2 = new MajorTickMark(){ Val = TickMarkValues.None };
            TickLabelPosition tickLabelPosition2 = new TickLabelPosition(){ Val = TickLabelPositionValues.NextTo };

            ChartShapeProperties chartShapeProperties5 = new ChartShapeProperties();

            A.Outline outline5 = new A.Outline(){ Width = 9525 };
            A.NoFill noFill1 = new A.NoFill();

            outline5.Append(noFill1);

            chartShapeProperties5.Append(outline5);

            TextProperties textProperties2 = new TextProperties();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties(){ Language = "en-GB" };

            paragraphProperties2.Append(defaultRunProperties2);
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties(){ Language = "en-US" };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(endParagraphRunProperties2);

            textProperties2.Append(bodyProperties2);
            textProperties2.Append(listStyle2);
            textProperties2.Append(paragraph2);
            CrossingAxis crossingAxis2 = new CrossingAxis(){ Val = (UInt32Value)132034944U };
            Crosses crosses2 = new Crosses(){ Val = CrossesValues.AutoZero };
            CrossBetween crossBetween1 = new CrossBetween(){ Val = CrossBetweenValues.Between };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(majorGridlines1);
            valueAxis1.Append(numberingFormat2);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(chartShapeProperties5);
            valueAxis1.Append(textProperties2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);

            plotArea1.Append(layout1);
            plotArea1.Append(lineChart1);
            plotArea1.Append(dateAxis1);
            plotArea1.Append(valueAxis1);

            Legend legend1 = new Legend();
            LegendPosition legendPosition1 = new LegendPosition(){ Val = LegendPositionValues.Bottom };

            Layout layout2 = new Layout();

            ManualLayout manualLayout2 = new ManualLayout();
            LeftMode leftMode2 = new LeftMode(){ Val = LayoutModeValues.Edge };
            TopMode topMode2 = new TopMode(){ Val = LayoutModeValues.Edge };
            Left left2 = new Left(){ Val = 2.1791557305336828E-2D };
            Top top2 = new Top(){ Val = 0.8117220024572126D };
            Width width2 = new Width(){ Val = 0.85363910761156092D };
            Height height2 = new Height(){ Val = 0.16055522670641476D };

            manualLayout2.Append(leftMode2);
            manualLayout2.Append(topMode2);
            manualLayout2.Append(left2);
            manualLayout2.Append(top2);
            manualLayout2.Append(width2);
            manualLayout2.Append(height2);

            layout2.Append(manualLayout2);

            TextProperties textProperties3 = new TextProperties();
            A.BodyProperties bodyProperties3 = new A.BodyProperties();
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties(){ Language = "en-GB" };

            paragraphProperties3.Append(defaultRunProperties3);
            A.EndParagraphRunProperties endParagraphRunProperties3 = new A.EndParagraphRunProperties(){ Language = "en-US" };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(endParagraphRunProperties3);

            textProperties3.Append(bodyProperties3);
            textProperties3.Append(listStyle3);
            textProperties3.Append(paragraph3);

            legend1.Append(legendPosition1);
            legend1.Append(layout2);
            legend1.Append(textProperties3);
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly(){ Val = true };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            return chart1;
        }
    }
}
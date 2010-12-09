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
        public override Chart GenerateChart(string title)
        {
            return null;
        }

        public Chart GenerateChart(string title, Dictionary<string, decimal> data)
        {
            int[] dates = { 35795, 35825, 35853, 35885, 35915, 35944, 35976, 36007, 36038, 36068 };
            float[] vals1 = { 0.13391881756578716F, 0.14135951028946941F, 0.13040516889825624F, 0.16154388221487725F, 0.15511067322277042F, 0.14587027748661438F, 0.13265738026184651F, 0.12819829560599968F, 0.15818540512656329F, 0.15639454380333218F };
            float[] vals2 = { 0.18240488665525859F, 0.13719636964421847F, 0.2023129960674655F, 0.25505764011250265F, 0.22470727329039256F, 0.1792123093706717F, 0.14553573405056669F, 9.9659954050917349E-2F, -1.7888654716325683E-2F, -6.6674939924710824E-2F };
            float[] vals3 = { 0.15224447832611041F, 0.13359515161058083F, 0.14098577339425858F, 0.17317018077742041F, 0.16727699126207812F, 0.15223997821064741F, 0.14086755873197027F, 0.11834256487264866F, 8.4171018307112461E-2F, 5.9370181760978025E-2F };
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
            DateAxis dateAxis1 = GenerateDateAxis(axisId1, AxisPositionValues.Bottom, "mmm\\-yy", axisId2, TickLabelPositionValues.Low);

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
    }
}


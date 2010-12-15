using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;

namespace RSMTenon.Graphing
{
    public class DrawdownLineChart : LineGraph
    {
        public Chart GenerateChart(string title)
        {
            int[] dates = { 35430, 35461, 35489, 35520, 35550, 35580, 35611, 35642, 35671, 35703 };
            float[] vals1 = { 0F, 0F, 0F, -1.4244555409299146E-2F, 0F, 0F, 0F, 0F, 0F, 0F };
            float[] vals2 = { 0F, 0F, -5.7615622628477414E-3F, -3.4050797058708955E-2F, 0F, 0F, 0F, 0F, -5.6756873038475431E-2F, -3.7456787148310035E-3F };
            float[] vals3 = { 0F, 0F, 0F, -1.0539275509624078E-2F, 0F, 0F, 0F, 0F, -2.5648874058951119E-3F, 0F };

            Chart chart1 = new Chart();

            Title title1 = GenerateTitle(title, 1200);
            PlotArea plotArea1 = new PlotArea();
            Layout layout2 = new Layout();

            LineChart lineChart1 = new LineChart();
            Grouping grouping1 = new Grouping() { Val = GroupingValues.Standard };

            ShowMarker showMarker1 = new ShowMarker() { Val = true };
            AxisId axisId1 = new AxisId() { Val = (UInt32Value)102222464U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)92672384U };

            lineChart1.Append(grouping1);
            lineChart1.Append(showMarker1);
            lineChart1.Append(axisId1);
            lineChart1.Append(axisId2);

            DateAxis dateAxis1 = GenerateDateAxis(axisId1, AxisPositionValues.Bottom, axisId2, TickLabelPositionValues.High);
            ValueAxis valueAxis1 = GenerateValueAxis(axisId2, AxisPositionValues.Left, axisId1);

            plotArea1.Append(layout2);
            plotArea1.Append(lineChart1);
            plotArea1.Append(dateAxis1);
            plotArea1.Append(valueAxis1);

            Legend legend1 = GenerateLegend(LegendPositionValues.Bottom);

            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };

            chart1.Append(title1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);

            return chart1;
        }
    }
}
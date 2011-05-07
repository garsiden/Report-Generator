using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml;

namespace RSMTenon.Graphing
{
    public class DrawdownLineChart : LineGraph
    {
        public DrawdownLineChart()
        {
            axisFormat = "mmm\\-yy";
            valueFormat = "0.0%";
            dateAxisFormat = "mmm\\-yy";
            valueAxisFormat = "0%";
            categoryName = "Date";
        }

        public Chart GenerateChart(string title)
        {
            // c:chart (Chart)
            Chart chart1 = new Chart();

            // c:title (Title)
            Title title1 = GenerateTitle(title, TITLE_FONT_SIZE);

            // c:plotArea (PlotArea)
            PlotArea plotArea1 = new PlotArea();
            // c:layout (Layout)
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
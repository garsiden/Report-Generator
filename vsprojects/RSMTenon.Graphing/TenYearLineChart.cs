﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml;
using RSMTenon.Data;

namespace RSMTenon.Graphing
{
    public class TenYearLineChart : LineGraph
    {
        public TenYearLineChart()
        {
            axisFormat = "mmm\\-yy";
            valueFormat = "0.0%";
            dateAxisFormat = "mmm\\-yy";
            valueAxisFormat = "0%";
            categoryName = "Date";
        }

        // Creates an Chart instance and adds its children.
        public Chart GenerateChart(string title)
        {
            // c:chart (Chart)
            Chart chart1 = new Chart();

            // c:title (Title)
            if (title != null) {
                Title title1 = GenerateTitle(title, TITLE_FONT_SIZE);
                chart1.Append(title1);
            } else {
                AutoTitleDeleted autoTitleDeleted1 = new AutoTitleDeleted() { Val = true };
                chart1.Append(autoTitleDeleted1);
            }

            // c:plotArea (PlotArea)
            PlotArea plotArea1 = new PlotArea();

            // c:layout (Layout)
            Layout layout2 = new Layout();

            // c:lineChart (LineChart)
            LineChart lineChart1 = new LineChart();

            // c:grouping (Grouping)
            Grouping grouping1 = new Grouping() { Val = GroupingValues.Standard };

            // c:marker (Marker)
            ShowMarker showMarker1 = new ShowMarker() { Val = true };

            // c:axId (AxisId)
            AxisId axisId1 = new AxisId() { Val = (UInt32Value)132034944U };
            AxisId axisId2 = new AxisId() { Val = (UInt32Value)132036480U };

            lineChart1.Append(grouping1);
            lineChart1.Append(showMarker1);
            lineChart1.Append(axisId1);
            lineChart1.Append(axisId2);

            // c:dateAx (DateAxis)
            DateAxis dateAxis1 = GenerateDateAxis(axisId1, AxisPositionValues.Bottom, axisId2, TickLabelPositionValues.Low);

            // c:valAx (ValueAxis)
            ValueAxis valueAxis1 = GenerateValueAxis(axisId2, AxisPositionValues.Left, axisId1);

            // c:plotArea (PlotArea)
            plotArea1.Append(layout2);
            plotArea1.Append(lineChart1);
            plotArea1.Append(dateAxis1);
            plotArea1.Append(valueAxis1);

            // c:legend (Legend)
            Legend legend1 = GenerateLegend(LegendPositionValues.Bottom);

            // c:plotVisOnly (PlotVisibleOnly)
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };

            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);

            return chart1;
        }
    }
}
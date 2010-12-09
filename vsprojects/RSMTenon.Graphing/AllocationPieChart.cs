using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;

namespace RSMTenon.Graphing
{
    public class AllocationPieChart : PieGraph
    {

        public override Chart GenerateChart(string title)
        {
            return null;
        }

        public Chart GenerateChart(string title, Dictionary<string, decimal> data)
        {
            Chart chart1 = new Chart();
            Title title1 = GenerateTitle(title);

            View3D view3D1 = new View3D();
            RotateX rotateX1 = new RotateX() { Val = 30 };
            Perspective perspective1 = new Perspective() { Val = 30 };

            view3D1.Append(rotateX1);
            view3D1.Append(perspective1);

            PlotArea plotArea1 = new PlotArea();
            Layout layout2 = new Layout();

            Pie3DChart pie3DChart1 = new Pie3DChart();
            VaryColors varyColors1 = new VaryColors() { Val = true };

            PieChartSeries pieChartSeries1 = new PieChartSeries();
            Index index1 = new Index() { Val = (UInt32Value)0U };
            Order order1 = new Order() { Val = (UInt32Value)0U };

            SeriesText seriesText1 = new SeriesText();
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = title + "Series";

            seriesText1.Append(numericValue1);

            CategoryAxisData categoryAxisData1 = new CategoryAxisData();

            StringLiteral stringLiteral1 = new StringLiteral();
            NumberLiteral numberLiteral1 = new NumberLiteral();

            UInt32 numPoints = (UInt32)data.Count();
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)numPoints };
            stringLiteral1.Append(pointCount1);

            PointCount pointCount2 = new PointCount() { Val = (UInt32Value)numPoints };
            numberLiteral1.Append(pointCount2);

            UInt32 i = 0U;
            foreach (var key in data.Keys) {
                StringPoint stringPoint1 = GenerateStringPoint(i, key);
                stringLiteral1.Append(stringPoint1);
                NumericPoint numericPoint1 = GenerateNumericPoint(i++, data[key].ToString());
                numberLiteral1.Append(numericPoint1);
            }

            categoryAxisData1.Append(stringLiteral1);

            Values values1 = new Values();
            FormatCode formatCode1 = new FormatCode();
            formatCode1.Text = "General";
            numberLiteral1.Append(formatCode1);
            values1.Append(numberLiteral1);

            pieChartSeries1.Append(index1);
            pieChartSeries1.Append(order1);
            pieChartSeries1.Append(seriesText1);
            pieChartSeries1.Append(categoryAxisData1);
            pieChartSeries1.Append(values1);

            pie3DChart1.Append(varyColors1);
            pie3DChart1.Append(pieChartSeries1);

            plotArea1.Append(layout2);
            plotArea1.Append(pie3DChart1);

            Legend legend1 = GenerateLegend(LegendPositionValues.Right);
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };

            chart1.Append(title1);
            chart1.Append(view3D1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);

            return chart1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;

namespace RSMTenon.Graph
{
    public class AllocationPieChart : Graph
    {

        // Creates an Chart instance and adds its children.
        public Chart GenerateChart(string title, Dictionary<string, decimal> data)
        {
            Chart chart1 = new Chart();
            Title title1 = new Title();
            ChartText chartText1 = new ChartText();

            RichText richText1 = new RichText();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties1 = new A.DefaultRunProperties() { FontSize = DEFAULT_FONT_SIZE };

            paragraphProperties1.Append(defaultRunProperties1);

            A.Run run1 = new A.Run();
            A.RunProperties runProperties1 = new A.RunProperties() { Language = "en-GB", FontSize = TITLE_FONT_SIZE };
            A.Text text1 = new A.Text();
            text1.Text = title;

            run1.Append(runProperties1);
            run1.Append(text1);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);

            richText1.Append(bodyProperties1);
            richText1.Append(listStyle1);
            richText1.Append(paragraph1);

            chartText1.Append(richText1);
            Layout layout1 = new Layout();

            title1.Append(chartText1);
            title1.Append(layout1);

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
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value) numPoints };
            stringLiteral1.Append(pointCount1);

            PointCount pointCount2 = new PointCount() { Val = (UInt32Value) numPoints };
            numberLiteral1.Append(pointCount2);

            UInt32 i = 0U;
            foreach (var key in data.Keys) {
                StringPoint stringPoint1 = generateStringPoint(i, key);
                stringLiteral1.Append(stringPoint1);
                NumericPoint numericPoint1 = generateNumericPoint(i++, data[key].ToString());
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

            Legend legend1 = new Legend();
            LegendPosition legendPosition1 = new LegendPosition() { Val = LegendPositionValues.Right };
            Layout layout3 = new Layout();

            TextProperties textProperties1 = new TextProperties();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties() { RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties();

            paragraphProperties2.Append(defaultRunProperties2);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = "en-GB" };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties2);
            textProperties1.Append(listStyle2);
            textProperties1.Append(paragraph2);

            legend1.Append(legendPosition1);
            legend1.Append(layout3);
            legend1.Append(textProperties1);
            PlotVisibleOnly plotVisibleOnly1 = new PlotVisibleOnly() { Val = true };

            chart1.Append(title1);
            chart1.Append(view3D1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            return chart1;
        
        }

        private StringPoint generateStringPoint(UInt32Value idx, string text)
        {
            StringPoint stringPoint1 = new StringPoint() { Index = idx };
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = text;
            stringPoint1.Append(numericValue1);

            return stringPoint1;
        }

        private NumericPoint generateNumericPoint(UInt32Value idx, string text)
        {
            NumericPoint numericPoint1 = new NumericPoint() { Index = idx };
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = text;
            numericPoint1.Append(numericValue1);

            return numericPoint1;
        }
    }
}

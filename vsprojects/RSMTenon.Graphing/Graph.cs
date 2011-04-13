using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RSMTenon.Data;

namespace RSMTenon.Graphing
{

    public class TextSeries { public string Name { get; set; } public List<double> Values { get; set; } }

    public abstract class Graph
    {
        public const long EMUS_PER_CENTIMETRE = 360000L;
        public const decimal DEFAULT_SIZE_X = 14.90M;
        public const decimal DEFAULT_SIZE_Y = 6.80M;
        protected const long DEFAULT_GRAPH_X = (long)(DEFAULT_SIZE_X * EMUS_PER_CENTIMETRE);
        protected const long DEFAULT_GRAPH_Y = (long)(DEFAULT_SIZE_Y * EMUS_PER_CENTIMETRE);
        public const string DEFAULT_LANG = "en-GB";
        protected const int TITLE_FONT_SIZE = 1200;
        protected const int DEFAULT_FONT_SIZE = 1100;

        public GraphData GraphData { get; set; }
        public static long Cx { get { return DEFAULT_GRAPH_X; } }
        public static long Cy { get { return DEFAULT_GRAPH_Y; } }

        protected string axisFormat = "General";
        protected string valueFormat = "General";
        protected string dateAxisFormat = "General";
        protected string valueAxisFormat = "General";
        protected string categoryAxisFormat = "General";
        protected string categoryName = "Category";

        protected uint order = 0U;
        protected uint index = 1U;

        public Graph()
        {
            GraphData = new GraphData("rId" + new Random().Next());
        }

        // c:title (Title)
        protected virtual Title GenerateTitle(string text)
        {
            return GenerateTitle(text, TITLE_FONT_SIZE);
        }

        protected virtual Title GenerateTitle(string text, Int32Value fontSize)
        {
            Title title1 = new Title();

            ChartText chartText1 = new ChartText();

            RichText richText1 = new RichText();
            A::BodyProperties bodyProperties1 = new A::BodyProperties();
            A::ListStyle listStyle1 = new A::ListStyle();

            A::Paragraph paragraph1 = new A::Paragraph();

            A::ParagraphProperties paragraphProperties1 = new A::ParagraphProperties();
            A::DefaultRunProperties defaultRunProperties1 = new A::DefaultRunProperties() { Language = DEFAULT_LANG, FontSize = DEFAULT_FONT_SIZE };

            paragraphProperties1.Append(defaultRunProperties1);

            A::Run run1 = new A::Run();
            A::RunProperties runProperties1 = new A::RunProperties() { Language = DEFAULT_LANG, FontSize = fontSize };
            A::Text text1 = new A::Text();
            text1.Text = text;

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

            return title1;
        }

        // c:legend (Legend)
        protected virtual Legend GenerateLegend(LegendPositionValues position)
        {
            Legend legend1 = new Legend();
            LegendPosition legendPosition1 = new LegendPosition() { Val = position };
            Layout layout1 = new Layout();

            TextProperties textProperties1 = new TextProperties();
            A::BodyProperties bodyProperties1 = new A::BodyProperties();
            A::ListStyle listStyle1 = new A::ListStyle();

            A::Paragraph paragraph1 = new A::Paragraph();

            A::ParagraphProperties paragraphProperties1 = new A::ParagraphProperties();
            A::DefaultRunProperties defaultRunProperties1 = new A::DefaultRunProperties() { Language = DEFAULT_LANG };

            paragraphProperties1.Append(defaultRunProperties1);
            A::EndParagraphRunProperties endParagraphRunProperties1 = new A::EndParagraphRunProperties() { Language = DEFAULT_LANG };

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties1);
            textProperties1.Append(listStyle1);
            textProperties1.Append(paragraph1);

            legend1.Append(legendPosition1);
            legend1.Append(layout1);
            legend1.Append(textProperties1);

            return legend1;
        }

        protected virtual StringPoint GenerateStringPoint(UInt32Value idx, string text)
        {
            StringPoint stringPoint1 = new StringPoint() { Index = idx };
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = text;
            stringPoint1.Append(numericValue1);

            return stringPoint1;
        }

        protected StringPoint GenerateStringPoint(uint index)
        {
            StringPoint stringPoint1 = new StringPoint() { Index = (UInt32Value)index };

            return stringPoint1;
        }

        protected virtual NumericPoint GenerateNumericPoint(UInt32Value idx, string text)
        {
            // c:pt (NumericPoint)
            NumericPoint numericPoint1 = new NumericPoint() { Index = idx };

            // c:v (NumericValue)
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = text;
            numericPoint1.Append(numericValue1);

            return numericPoint1;
        }

        protected virtual Values GenerateValues(string formatCode, double[] data, string columnName)
        {
            Values values1 = new Values();
            uint numPoints = (uint)data.Length;

            Formula formula1 = new Formula();
            formula1.Text = formulaColumn(columnName, 2, numPoints);

            // c:numRef (NumberingReference)
            NumberReference numberReference2 = new NumberReference();
            NumberingCache numberingCache2 = GenerateNumberingCache(formatCode, numPoints);

            for (uint i = 0; i < numPoints; i++) {
                NumericPoint numericPoint = GenerateNumericPoint(i, data[i].ToString());
                numberingCache2.Append(numericPoint);
            }

            numberReference2.Append(formula1);
            numberReference2.Append(numberingCache2);
            values1.Append(numberReference2);

            return values1;
        }

        protected NumberingCache GenerateNumberingCache(string formatCode, uint numPoints)
        {
            // c:numCache (NumberingCache)
            NumberingCache numberingCache2 = new NumberingCache();
            FormatCode formatCode2 = new FormatCode();
            formatCode2.Text = formatCode;
            PointCount pointCount3 = new PointCount() { Val = (UInt32Value)numPoints };
            numberingCache2.Append(formatCode2);
            numberingCache2.Append(pointCount3);

            return numberingCache2;
        }

        protected SeriesText GenerateSeriesText(string seriesName, string columnName)
        {
            SeriesText seriesText1 = new SeriesText();

            StringReference stringReference1 = new StringReference();
            Formula formula1 = new Formula();
            formula1.Text = formulaColumn(columnName, 1);

            StringCache stringCache1 = new StringCache();
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)1U };

            StringPoint stringPoint1 = new StringPoint() { Index = (UInt32Value)0U };
            NumericValue numericValue1 = new NumericValue();
            numericValue1.Text = seriesName;

            stringPoint1.Append(numericValue1);

            stringCache1.Append(pointCount1);
            stringCache1.Append(stringPoint1);

            stringReference1.Append(formula1);
            stringReference1.Append(stringCache1);

            seriesText1.Append(stringReference1);
            return seriesText1;
        }

        protected virtual Layout GeneratePlotAreaLayout()
        {
            Layout layout1 = new Layout();

            return layout1;
        }

        protected CategoryAxisData GenerateCategoryAxisData(string formatCode, int[] data, string columnName)
        {
            CategoryAxisData categoryAxisData1 = new CategoryAxisData();

            uint numPoints = (uint)data.Length;
            string form = formulaColumn(columnName, 2, numPoints);

            NumberReference numberReference1 = new NumberReference();
            Formula formula1 = new Formula();
            formula1.Text = form;

            NumberingCache numberingCache1 = GenerateNumberingCache(formatCode, numPoints);

            for (uint i = 0U; i < numPoints; i++) {
                NumericPoint numericPoint = GenerateNumericPoint(i, data[i].ToString());
                numberingCache1.Append(numericPoint);
            }

            numberReference1.Append(formula1);
            numberReference1.Append(numberingCache1);
            categoryAxisData1.Append(numberReference1);

            return categoryAxisData1;
        }

        protected CategoryAxisData GenerateCategoryAxisData(IEnumerable<string> data, string columnName)
        {
            CategoryAxisData categoryAxisData1 = new CategoryAxisData();
            StringReference stringReference1 = new StringReference();
            StringCache stringCache1 = new StringCache();

            uint i = 0U;
            foreach (string item in data) {
                StringPoint stringPoint1 = GenerateStringPoint(i++);
                NumericValue numericValue1 = new NumericValue() { Text = item };
                stringPoint1.Append(numericValue1);
                stringCache1.Append(stringPoint1);
            }

            uint count = i++;
            PointCount pointCount1 = new PointCount() { Val = (UInt32Value)count };
            stringCache1.InsertAt(pointCount1, 0);

            Formula formula1 = new Formula();
            formula1.Text = formulaColumn(columnName, 2, count);
            stringReference1.Append(formula1);
            stringReference1.Append(stringCache1);

            categoryAxisData1.Append(stringReference1);
            return categoryAxisData1;
        }

        protected ChartShapeProperties GenerateChartShapeProperties(int width)
        {
            ChartShapeProperties chartShapeProperties5 = new ChartShapeProperties();

            A::Outline outline5 = new A::Outline() { Width = width };

            A::SolidFill solidFill7 = new A::SolidFill();
            A::RgbColorModelHex rgbColorModelHex7 = new A::RgbColorModelHex() { Val = "000000" };

            solidFill7.Append(rgbColorModelHex7);
            A::PresetDash presetDash4 = new A::PresetDash() { Val = A::PresetLineDashValues.Solid };

            outline5.Append(solidFill7);
            outline5.Append(presetDash4);

            chartShapeProperties5.Append(outline5);

            return chartShapeProperties5;
        }

        protected ChartShapeProperties GenerateChartShapeProperties(string colourHex)
        {
            ChartShapeProperties chartShapeProperties1 = new ChartShapeProperties();

            A::Outline outline1 = new A::Outline();
            A::SolidFill solidFill1 = new A::SolidFill();
            A::RgbColorModelHex rgbColorModelHex1 = new A::RgbColorModelHex() { Val = colourHex };
            solidFill1.Append(rgbColorModelHex1);
            outline1.Append(solidFill1);
            chartShapeProperties1.Append(outline1);

            return chartShapeProperties1;
        }

        protected string formulaColumn(string column, int row)
        {
            string col = column.ToUpper();
            string formula = String.Format("{0}!${1}${2}", GraphData.SHEETNAME, col, row);

            return formula;
        }

        protected string formulaColumn(string column, int startRow, uint length)
        {
            string col = column.ToUpper();
            string formula = String.Format("{0}!${1}${2}:${3}${4}", GraphData.SHEETNAME, col, startRow, col, length + 1);

            return formula;
        }
    }
}

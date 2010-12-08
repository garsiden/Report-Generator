using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;

namespace RSMTenon.Graphing
{
    public abstract class Graph
    {
        private const Int64 EMUS_PER_CENTIMETRE = 360000L;

        public const Int64 LARGE_GRAPH_X = 5486400L;
        public const Int64 LARGE_GRAPH_Y = 3200400L;

        public const Int64 SMALL_GRAPH_X = (Int64)(14.85 * EMUS_PER_CENTIMETRE);
        public const Int64 SMALL_GRAPH_Y = (Int64) (6.71 * EMUS_PER_CENTIMETRE);

        public const Int64 DEFAULT_GRAPH_X = (Int64)(14.90 * EMUS_PER_CENTIMETRE);
        public const Int64 DEFAULT_GRAPH_Y = (Int64)(6.80 * EMUS_PER_CENTIMETRE);

        protected const int TITLE_FONT_SIZE = 1100;
        protected const int DEFAULT_FONT_SIZE = 1100;

        
        public static Int64 Cx { get { return DEFAULT_GRAPH_X; } }
        public static Int64 Cy { get { return DEFAULT_GRAPH_Y; } }

        protected const string DEFAULT_LANG = "en-GB";

        // c:title (Title)

        public Title GenerateTitle(string text)
        {
            return GenerateTitle(text, TITLE_FONT_SIZE);
        }

        public Title GenerateTitle(string text, Int32Value fontSize)
        {
            Title title1 = new Title();

            ChartText chartText1 = new ChartText();

            RichText richText1 = new RichText();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties1 = new A.DefaultRunProperties() { Language = DEFAULT_LANG, FontSize = DEFAULT_FONT_SIZE };

            paragraphProperties1.Append(defaultRunProperties1);

            A.Run run1 = new A.Run();
            A.RunProperties runProperties1 = new A.RunProperties() { Language = DEFAULT_LANG, FontSize = fontSize };
            A.Text text1 = new A.Text();
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

        // Creates an Legend instance and adds its children.
        public Legend GenerateLegend(LegendPositionValues position)
        {
            Legend legend1 = new Legend();
            LegendPosition legendPosition1 = new LegendPosition() { Val = position };
            Layout layout1 = new Layout();

            TextProperties textProperties1 = new TextProperties();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties1 = new A.DefaultRunProperties() { Language = DEFAULT_LANG };

            paragraphProperties1.Append(defaultRunProperties1);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = DEFAULT_LANG };

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

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;

namespace RSMTenon.Graphing
{
    public class BarGraph : Graph
    {
        public ChartShapeProperties GenerateChartShapeProperties(string colourHex, int width)
        {
            ChartShapeProperties chartShapeProperties2 = new ChartShapeProperties();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = colourHex };
            solidFill1.Append(rgbColorModelHex1);

            A.Outline outline2 = new A.Outline() { Width = width };
            A.SolidFill solidFill2 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill2.Append(rgbColorModelHex2);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline2.Append(solidFill2);
            outline2.Append(presetDash1);

            chartShapeProperties2.Append(solidFill1);
            chartShapeProperties2.Append(outline2);

            return chartShapeProperties2;
        }


    }
}

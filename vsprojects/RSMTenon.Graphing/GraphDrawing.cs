using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;

namespace RSMTenon.Graphing
{
    public class GraphDrawing
    {
        public static Drawing GenerateDrawing(string id, string name, uint docPrId, long cx, long cy)
        {
            // w:drawing (Drawing)
            Drawing drawing1 = new Drawing();

            // wp:inline (Inline)
            Wp::Inline inline1 = new Wp::Inline();
            // wp:extent (Extent)
            Wp::Extent extent1 = new Wp::Extent() { Cx = cx, Cy = cy };
            // wp:docPr (DocProperties)
            Wp::DocProperties docProperties1 = new Wp::DocProperties() { Id = docPrId, Name = name };

            // a:graphic (Graphic)
            A::Graphic graphic1 = new A::Graphic();
            graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            // a:graphicData (GraphicData)
            A::GraphicData graphicData1 = new A::GraphicData() { Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" };

            // c:chart (ChartReference)
            C::ChartReference chartReference1 = new C::ChartReference() { Id = id };
            chartReference1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartReference1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            graphicData1.Append(chartReference1);

            graphic1.Append(graphicData1);

            inline1.Append(extent1);
            inline1.Append(docProperties1);
            inline1.Append(graphic1);

            drawing1.Append(inline1);

            return drawing1;
        }
    }
}

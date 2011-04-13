using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using C = DocumentFormat.OpenXml.Drawing.Charts;

namespace RSMTenon.Graphing
{
    public class GraphSpace
    {
        public static C::ChartSpace GenerateChartSpace(C::Chart chart)
        {
            return GenerateChartSpace(chart, false);
        }

        public static C::ChartSpace GenerateChartSpace(C::Chart chart, bool withDate1904)
        {
            // c:chartSpace (ChartSpace)            
            C::ChartSpace chartSpace1 = new C::ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            C::Date1904 date1904 = null;
            if (withDate1904) {
                date1904 = generateDate1904();
            }

            // c:lang (EditingLanguage)
            C::EditingLanguage editingLanguage1 = new C::EditingLanguage() { Val = Graph.DEFAULT_LANG };

            if (date1904 != null) {
                chartSpace1.Append(date1904);
            }
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(chart);

            return chartSpace1;
        }

        public static C::ChartSpace GenerateChartSpaceWithData(C::Chart chart, string externalDataId)
        {
            // c:chartSpace (ChartSpace)            
            C::ChartSpace chartSpace1 = new C::ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            // c:lang (EditingLanguage)
            C::EditingLanguage editingLanguage1 = new C::EditingLanguage() { Val = Graph.DEFAULT_LANG };

            C::ExternalData externalData1 = new C::ExternalData() { Id = externalDataId };

            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(chart);
            chartSpace1.Append(externalData1);

            return chartSpace1;
        }

        // Creates an Date1904 instance and adds its children.
        private static C::Date1904 generateDate1904()
        {
            C::Date1904 date19041 = new C::Date1904() { Val = true };
            return date19041;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using C = DocumentFormat.OpenXml.Drawing.Charts;

namespace RSMTenon.Graphing
{
    public class GraphSpace
    {
        public static C.ChartSpace GenerateChartSpace(ChartPart part)
        {
            return GenerateChartSpace(part, false);
        }

        public static C.ChartSpace GenerateChartSpace(ChartPart part, bool withDate1904)
        {
            // c:chartSpace (ChartSpace)            
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            C.Date1904 date1904 = null;            
            if (withDate1904) {
                date1904 = generateDate1904();
            }

            // c:lang (EditingLanguage)
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = Graph.DEFAULT_LANG };

            // c:printSettings (PrintSettings)
            C.PrintSettings printSettings1 = new C.PrintSettings();
            C.HeaderFooter headerFooter1 = new C.HeaderFooter();
            C.PageMargins pageMargins1 = new C.PageMargins() { Left = 0.70D, Right = 0.70D, Top = 0.75D, Bottom = 0.75D, Header = 0.30D, Footer = 0.30D };
            C.PageSetup pageSetup1 = new C.PageSetup();

            printSettings1.Append(headerFooter1);
            printSettings1.Append(pageMargins1);
            printSettings1.Append(pageSetup1);

            if (date1904 != null) {
                chartSpace1.Append(date1904);
            }
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(printSettings1);

            return chartSpace1;
        }

        // Creates an Date1904 instance and adds its children.
        private static C.Date1904 generateDate1904()
        {
            C.Date1904 date19041 = new C.Date1904() { Val = true };
            return date19041;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using d = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
//using DocumentFormat.OpenXml.Spreadsheet;


namespace ImportChartFromExcelToWord
{
    class Program
    {
        static string path = @"C:\Documents and Settings\garsiden\My Documents\Projects\RepGen\test\chart\";
        static string template = path + "tenyear_template.docx";
        static string outputDoc = path + "tenyear_out.docx";
        static string source = path + "tenyear.xlsx";

        static void Main(string[] args)
        {
            //string outputDoc = docName; // path + "output.docx";
            File.Copy(template, outputDoc, true);

            ImportChartFromSpreadsheet(source, outputDoc);
        }

        static void ImportChartFromSpreadsheet(string spreadsheetFileName, string wordFileName)
        {
            //Open Word document
            using (WordprocessingDocument myWordDoc = WordprocessingDocument.Open(wordFileName, true))
            {
                //Find the content control that will contain the chart
                MainDocumentPart mainPart = myWordDoc.MainDocumentPart;
                SdtBlock sdt = mainPart.Document.Descendants<SdtBlock>().Where(
                         s => s.SdtProperties.GetFirstChild<SdtAlias>().Val.Value.Equals("TenYearChart")).First();

                //Nuke the placeholder content of the content control
                Paragraph p = sdt.SdtContentBlock.GetFirstChild<Paragraph>();
                p.RemoveAllChildren();

                //Create a new run that has an inline drawing object
                Run r = new Run();
                p.Append(r);
                Drawing drawing = new Drawing();
                r.Append(drawing);
                //These dimensions work perfectly for my template document
                wp.Inline inline = new wp.Inline(
                                        new wp.Extent() 
                                            { Cx = 5486400, Cy = 3200400 });

                //Open Excel spreadsheet
                using (SpreadsheetDocument mySpreadsheet = SpreadsheetDocument.Open(spreadsheetFileName, true))
                {
                    //Get all the appropriate parts
                    WorkbookPart workbookPart = mySpreadsheet.WorkbookPart;
                    //WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById("rId1");
                    var worksheetPart = getWorksheetPartFromName(mySpreadsheet, "Defensive Charts");
                    DrawingsPart drawingPart = worksheetPart.DrawingsPart;
                    ChartPart chartPart = drawingPart.ChartParts.First();
                    //ChartPart chartPart = (ChartPart)drawingPart.GetPartById("rId2");

                    //Clone the chart part and add it to my Word document
                    ChartPart importedChartPart = mainPart.AddPart<ChartPart>(chartPart);
                    string relId = mainPart.GetIdOfPart(importedChartPart);

                    //The frame element contains information for the chart
                    GraphicFrame frame = drawingPart.WorksheetDrawing.Descendants<GraphicFrame>().First();
                    string chartName = frame.NonVisualGraphicFrameProperties.NonVisualDrawingProperties.Name;
                    //Clone this node so we can add it to my Word document
                    d.Graphic clonedGraphic = (d.Graphic)frame.Graphic.CloneNode(true);
                    ChartReference c = clonedGraphic.GraphicData.GetFirstChild<ChartReference>();
                    c.Id = relId;

                    //Give the chart a unique id and name
                    wp.DocProperties docPr = new wp.DocProperties();
                    docPr.Name = chartName;
                    docPr.Id = GetMaxDocPrId(mainPart) + 1;

                    //add the chart data to the inline drawing object
                    inline.Append(docPr, clonedGraphic);
                    drawing.Append(inline);
                }
                mainPart.Document.Save();
            }
        }

        static WorksheetPart getWorksheetPartFromName(SpreadsheetDocument doc, string name)
        {
            var sheets = doc.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Where(s => s.Name == name);
            if (sheets.Count() == 0) {
                // The specified worksheet does not exist.
                return null;
            }
            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)doc.WorkbookPart.GetPartById(relationshipId);

            return worksheetPart;
        }

        static uint GetMaxDocPrId(MainDocumentPart mainPart)
        {
            uint max = 1;

            //Get max id value of docPr elements 
            foreach (wp.DocProperties docPr in mainPart.Document.Descendants<wp.DocProperties>())
            {
                uint id = docPr.Id;
                if (id > max)
                    max = id;
            }
            return max; 
        }
    }
}

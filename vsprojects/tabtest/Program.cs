using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace RSMTenon.ReportGenerator
{

    class Program
    {
        private static string docName = @"C:\Documents and Settings\garsiden\My Documents\Projects\RepGen\test\TableTest.docx";

        static void Main(string[] args)
        {
            Program This = new Program();
            This.AddTableToDoc();
        }

        public void AddTableToDoc()
        {
            // open document and create table
            WordprocessingDocument myDoc = WordprocessingDocument.Open(docName, true);
            MainDocumentPart mainPart = myDoc.MainDocumentPart;
            Document doc = mainPart.Document;

            // create table to hold strategy details
            StrategyTable stratTab = new StrategyTable();
            string[] colwidths = {"3700", "980", "1180", "1120", "1480", "1120", "1160"};
            Table table1 = stratTab.CreateTable(colwidths);

            // create a header row
            CellProps[] cellProps = new CellProps[] {
            new CellProps() { align = JustificationValues.Left },
            new CellProps(),
            new CellProps() { text = "Weighting" },
            new CellProps() { text = "Amount" },
            new CellProps() { text = "Expected Yield*" },
            new CellProps() { text = "Projected Income*" }
            };
            TableRow header = stratTab.GenerateTableHeaderRow(cellProps, 465U);
            table1.Append(header);

            //  create an asset class section
            cellProps = new CellProps[] {
            new CellProps() { text = "Cash", align = JustificationValues.Left },
            new CellProps() { text = "1.5%" },
            new CellProps(),
            new CellProps(),
            new CellProps(),
            new CellProps()

            };
            header = stratTab.GenerateTableHeaderRow(cellProps, 255U);
            table1.Append(header);

            // create a content row
            cellProps = new CellProps[] {
            new CellProps() { span = 2, text = "Aviva Emerging Market Local Currency Bond Fund", align = JustificationValues.Left },
            new CellProps() { span = 0 },
            new CellProps() { text = "1.50%" },
            new CellProps() { text = "£15,000" },
            new CellProps() { text = "0.10%" },
            new CellProps() { text = "£15.00" }
            };

            TableRow row = stratTab.GenerateTableRow(cellProps, 255U);
            table1.Append(row);

            // create a footer row
            cellProps = new CellProps[] {
            new CellProps(),
            new CellProps() { text= "100.0%" },
            new CellProps(),
            new CellProps() { text = "£,1000,000", boxed = true },
            new CellProps(),
            new CellProps() { text = "£24,779", boxed = true }
            };

            TableRow footer = stratTab.GenerateTableFooterRow(cellProps, 255U);
            table1.Append(footer);
            
            // add table to doc and save
            List<SdtBlock> stdList =
                mainPart.Document.Descendants<SdtBlock>()
                .Where(s => "portfolio"
                .Contains
                (s.SdtProperties.GetFirstChild<SdtAlias>().Val.Value)).ToList();

            if (stdList.Count != 0) {
                SdtBlock sdt = stdList.First<SdtBlock>();
                OpenXmlElement parent = sdt.Parent;
                parent.InsertAfter(table1, sdt);
                //sdt.Remove();
            }
                
            doc.Save();
        }

        void GenerateTableFormDB()
        {
            using (WordprocessingDocument myDoc = WordprocessingDocument.Open(docName, true)) {
                MainDocumentPart mainPart = myDoc.MainDocumentPart;
                Document doc = mainPart.Document;
                //Create new table with predefined table style 
                Table table = new Table();
                TableProperties tblPr = new TableProperties();
                TableStyle tblStyle = new TableStyle() { Val = "MyTableStyle" };
                tblPr.AppendChild(tblStyle);
                table.AppendChild(tblPr);
                string[] headerContent = new string[] { "Code", "Name", "Scientific Name" };
                //Create header row 
                TableRow header = CreateRow(headerContent);
                table.AppendChild(header);

                //Connect to database
                BirdTrackDataContext db = new BirdTrackDataContext();
                var btoQuery =
                     from b in db.btos
                     select b;

                //For every product in my database create a new row in my table
                foreach (var item in btoQuery) {
                    //                            price += Math.Round(item.ListPrice, 2);

                    string[] content = new[] { item.bto_code, 
                            item.species_name,
                            item.scientific_name};
                    TableRow tr = CreateRow(content);
                    table.AppendChild(tr);
                }

                doc.Body.AppendChild(table);
                doc.Save();
            }
        }


        //This method allows me to create either a row full of text cells or a row of text cells and a last row with a drawing
        TableRow CreateRow(string[] cellText)
        {
            TableRow tr = new TableRow();

            //create cells with simple text
            foreach (string s in cellText) {
                TableCell tc = new TableCell();
                Paragraph p = new Paragraph();
                Run r = new Run();
                Text t = new Text(s);

                r.AppendChild(t);
                p.AppendChild(r);
                tc.AppendChild(p);
                tr.AppendChild(tc);
            }

            return tr;
        }
    }
}

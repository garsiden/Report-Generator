using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RSMTenon.Data;

namespace RSMTenon.Graphing
{
    public class GraphData
    {

        public static SpreadsheetDocument GenerateDataSpreadsheet(Stream stream)
        {
            // Open a SpreadsheetDocument based on a stream.
            SpreadsheetDocument spreadsheetDocument =
                SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "mySheet" };
            sheets.Append(sheet);

            workbookpart.Workbook.Save();

            // Close the document.
            //spreadsheetDocument.Close();

            return spreadsheetDocument;
        }


        public void Add(SpreadsheetDocument spreadsheetDoc)
        {
            WorkbookPart part = spreadsheetDoc.WorkbookPart;
            SharedStringTablePart sharedStringTablePart1 = part.AddNewPart<SharedStringTablePart>();
            GenerateSharedStringTable(sharedStringTablePart1);
            AddSheetData(spreadsheetDoc);
        }

        private void GenerateSharedStringTable(SharedStringTablePart sharedStringTablePart1)
        {
            SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = (UInt32Value)5U, UniqueCount = (UInt32Value)5U };

            SharedStringItem sharedStringItem1 = new SharedStringItem();
            Text text1 = new Text();
            text1.Text = "Sales";

            sharedStringItem1.Append(text1);

            SharedStringItem sharedStringItem2 = new SharedStringItem();
            Text text2 = new Text();
            text2.Text = "1st Qtr";

            sharedStringItem2.Append(text2);

            SharedStringItem sharedStringItem3 = new SharedStringItem();
            Text text3 = new Text();
            text3.Text = "2nd Qtr";

            sharedStringItem3.Append(text3);

            SharedStringItem sharedStringItem4 = new SharedStringItem();
            Text text4 = new Text();
            text4.Text = "3rd Qtr";

            sharedStringItem4.Append(text4);

            SharedStringItem sharedStringItem5 = new SharedStringItem();
            Text text5 = new Text();
            text5.Text = "4th Qtr";

            sharedStringItem5.Append(text5);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);
            sharedStringTable1.Append(sharedStringItem5);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        public void AddSheetData(SpreadsheetDocument spreadsheetDoc)
        {
            Sheet sheet = (Sheet)spreadsheetDoc.WorkbookPart.Workbook.Sheets.First();
            string sheetId = sheet.Id;

            WorksheetPart wsp = (WorksheetPart)spreadsheetDoc.WorkbookPart.Parts
                .Where(pt => pt.RelationshipId == sheetId).FirstOrDefault()
                .OpenXmlPart;
            SheetData sheetData1 = (SheetData)wsp.Worksheet.Elements().FirstOrDefault();

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell1 = new Cell() { CellReference = "B1", DataType = CellValues.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "0";

            cell1.Append(cellValue1);

            row1.Append(cell1);

            Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell2 = new Cell() { CellReference = "A2", DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "1";

            cell2.Append(cellValue2);

            Cell cell3 = new Cell() { CellReference = "B2" };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "8.1999999999999993";

            cell3.Append(cellValue3);

            row2.Append(cell2);
            row2.Append(cell3);

            Row row3 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell4 = new Cell() { CellReference = "A3", DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "2";

            cell4.Append(cellValue4);

            Cell cell5 = new Cell() { CellReference = "B3" };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "3.2";

            cell5.Append(cellValue5);

            row3.Append(cell4);
            row3.Append(cell5);

            Row row4 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell6 = new Cell() { CellReference = "A4", DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "3";

            cell6.Append(cellValue6);

            Cell cell7 = new Cell() { CellReference = "B4" };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "1.4";

            cell7.Append(cellValue7);

            row4.Append(cell6);
            row4.Append(cell7);

            Row row5 = new Row() { RowIndex = (UInt32Value)5U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell8 = new Cell() { CellReference = "A5", DataType = CellValues.SharedString };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = "4";

            cell8.Append(cellValue8);

            Cell cell9 = new Cell() { CellReference = "B5" };
            CellValue cellValue9 = new CellValue();
            cellValue9.Text = "1.2";

            cell9.Append(cellValue9);

            row5.Append(cell8);
            row5.Append(cell9);

            sheetData1.Append(row1);
            sheetData1.Append(row2);
            sheetData1.Append(row3);
            sheetData1.Append(row4);
            sheetData1.Append(row5);
            //return sheetData1;
        }

    }
}

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
        private SpreadsheetDocument spreadsheetDoc;
        private char dataColumn = 'B';
        public Stream BinaryStream { get; private set; }
        private WorksheetPart wsp;
        private SharedStringTablePart sharedStringTablePart;
        public string ExternalDataId { get; private set; }

        public GraphData(string externalDataId)
        {
            ExternalDataId = externalDataId;
            BinaryStream = new MemoryStream();
            spreadsheetDoc = createSpreadsheet(BinaryStream, "Sheet1");
            wsp = getWorksheetPart(spreadsheetDoc);

            // Add SharedStringTablePart
            WorkbookPart workbookPart = spreadsheetDoc.WorkbookPart;
            sharedStringTablePart = workbookPart.SharedStringTablePart;

        }

        public void AddTextColumn(string[] text)
        {
            int index;
            Cell cell;
            uint rowIndex = 2U;

            // add column header cell
            addColumnHeader("A", "Categories");
 
            // Get the SharedStringTablePart. If it does not exist, create a new one.
            SharedStringTablePart shareStringPart;
            if (spreadsheetDoc.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0) {
                shareStringPart = spreadsheetDoc.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
            } else {
                shareStringPart = spreadsheetDoc.WorkbookPart.AddNewPart<SharedStringTablePart>();
            }


            foreach (var item in text) {
                // add string to shared string table, creating table if required
                index = InsertSharedStringItem(item, shareStringPart);
                cell = InsertCellInWorksheet("A", rowIndex++, wsp);
                cell.CellValue = new CellValue((index).ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }
        }

        public void AddDataColumn(string columnHeader, double?[] data)
        {
            // get next column
            string columnName = (dataColumn++).ToString();

            // add column header cell
            addColumnHeader(columnName, columnHeader);

            // add data cells
            uint numRows = (uint)data.Length;
            int j = 0;

            for (uint index = 2U; index < (numRows + 2); index++) {
                Cell cell = InsertCellInWorksheet(columnName, index, wsp);
                double val = data[j++] ?? 0D;
                cell.CellValue = new CellValue(val.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
            }
            wsp.Worksheet.Save();
        }

        private void addColumnHeader(string columnName, string header)
        {
            // Get the SharedStringTablePart. If it does not exist, create a new one.
            SharedStringTablePart shareStringPart;
            if (spreadsheetDoc.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0) {
                shareStringPart = spreadsheetDoc.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
            } else {
                shareStringPart = spreadsheetDoc.WorkbookPart.AddNewPart<SharedStringTablePart>();
            }

            // add column heading cell
            int index = InsertSharedStringItem(header, shareStringPart);
            Cell cell = InsertCellInWorksheet(columnName, 1U, wsp);
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.CellValue = new CellValue() { Text = index.ToString() };
        }

        public void AddEmbeddedToChartPart(ChartPart part)
        {
            EmbeddedPackagePart embeddedPackagePart1 = part.AddNewPart<EmbeddedPackagePart>("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExternalDataId);
            BinaryStream.Position = 0;
            embeddedPackagePart1.FeedData(BinaryStream);
        }

        public void WriteSpreadSheetToFile(string filepath)
        {
            copyStream(BinaryStream, filepath);
        }

        private static SpreadsheetDocument createSpreadsheet(Stream stream, string sheetName)
        {
            return GraphData.GenerateDataSpreadsheet(stream);
        }
 
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
            Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
            sheets.Append(sheet);

           // 
            
            //workbookpart.AddNewPart<SharedStringTablePart>("rIdSST");
            workbookpart.Workbook.Save();

            // Close the document.
            //spreadsheetDocument.Close();

            return spreadsheetDocument;
        }


        private WorksheetPart getWorksheetPart(SpreadsheetDocument sreadsheetDoc)
        {
            WorkbookPart part = spreadsheetDoc.WorkbookPart;

            Sheet sheet = (Sheet)part.Workbook.Sheets.First();
            string sheetId = sheet.Id;

            WorksheetPart worksheetPart = (WorksheetPart)spreadsheetDoc.WorkbookPart.Parts
                .Where(pt => pt.RelationshipId == sheetId).FirstOrDefault()
                .OpenXmlPart;

            return worksheetPart;
        }

        public void Close()
        {
            spreadsheetDoc.Close();
        }

        public void Add(SpreadsheetDocument spreadsheetDoc, List<AssetWeighting> model)
        {
            WorkbookPart part = spreadsheetDoc.WorkbookPart;
            SharedStringTablePart sharedStringTablePart1 = part.AddNewPart<SharedStringTablePart>();
            string[] items = { "My Sales", "Qtr 1", "Qtr 2", "Qtr 3", "Qtr 4" };
            var data = model.Select(m => m.Weighting).ToArray<double?>();
            GenerateSharedStringTable(sharedStringTablePart1, items);

            Sheet sheet = (Sheet)part.Workbook.Sheets.First();
            string sheetId = sheet.Id;

            WorksheetPart wsp = (WorksheetPart)spreadsheetDoc.WorkbookPart.Parts
                .Where(pt => pt.RelationshipId == sheetId).FirstOrDefault()
                .OpenXmlPart;

            // add column heading cell
            Cell cell = InsertCellInWorksheet("B", 1U, wsp);
            cell.DataType = CellValues.SharedString;
            CellValue cellValue1 = new CellValue("0");
            cellValue1.Text = "0";
            wsp.Worksheet.Save();

            AddSharedStringColumnData(wsp, "A", 2, 4);
            AddColumnData(wsp, "B", 2, data);
            //AddSheetData(spreadsheetDoc);
        }

        private void GenerateSharedStringTable(SharedStringTablePart sharedStringTablePart1, string[] items)
        {
            uint num = (uint)items.Length;
            SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = (UInt32Value)num, UniqueCount = (UInt32Value)num };

            foreach (var item in items) {
                SharedStringItem sharedStringItem1 = new SharedStringItem();
                Text text1 = new Text() { Text = item };
                sharedStringItem1.Append(text1);
                sharedStringTable1.Append(sharedStringItem1);
            }


            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }


        protected void AddSharedStringColumnData(WorksheetPart worksheetPart, string columnName, uint startRow, uint numRows)
        {
            for (uint index = startRow; index < (startRow + numRows); index++) {
                Cell cell = InsertCellInWorksheet(columnName, index, worksheetPart);
                cell.CellValue = new CellValue((index - 1).ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }
            worksheetPart.Worksheet.Save();
        }

        protected void AddColumnData(WorksheetPart worksheetPart, string columnName, uint startRow, double?[] data)
        {
            uint numRows = (uint)data.Length;
            int j = 0;

            for (uint index = startRow;  index < (startRow + numRows); index++) {
                Cell cell = InsertCellInWorksheet(columnName, index, worksheetPart);
                double val = data[j++] ?? 0D;
                cell.CellValue = new CellValue(val.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
            }
            worksheetPart.Worksheet.Save();
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
        }

        // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        // If the cell already exists, returns it. 
        //private Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        private Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0) {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            } else {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0) {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            } else {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>()) {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0) {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        // Given text and a SharedStringTablePart, creates a SharedStringItem with the specified text 
        // and inserts it into the SharedStringTablePart. If the item already exists, returns its index.
        private int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // If the part does not contain a SharedStringTable, create it.
            if (shareStringPart.SharedStringTable == null) {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>()) {
                if (item.InnerText == text) {
                    // The text already exists in the part. Return its index.
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        public static void copyStream(Stream input, string destination)
        {
            input.Position = 0;
            using (FileStream output = new FileStream(destination, FileMode.Create, FileAccess.Write)) {
                byte[] buffer = new byte[16384];
                int len;
                while ((len = input.Read(buffer, 0, buffer.Length)) > 0) {
                    output.Write(buffer, 0, len);
                }
            }
        }

        ~GraphData()
        {
            if (BinaryStream != null)
                BinaryStream.Close();
        }
    }
}

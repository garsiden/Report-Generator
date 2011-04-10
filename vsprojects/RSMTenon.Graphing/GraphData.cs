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
        private WorksheetPart worksheetPart;
        private SharedStringTablePart shareStringPart;
        private char dataColumn = 'B';

        public Stream BinaryStream { get; private set; }
        public string ExternalDataId { get; private set; }

        public GraphData(string externalDataId)
        {
            ExternalDataId = externalDataId;
            BinaryStream = new MemoryStream();
            createSpreadsheet(BinaryStream, "Sheet1");
        }

        public void AddTextColumn(string[] text)
        {
            int index;
            Cell cell;
            uint rowIndex = 2U;

            // add column header cell
            addColumnHeader("A", "Categories");

            foreach (var item in text) {
                // add string to shared string table, creating table if required
                index = insertSharedStringItem(item);
                cell = insertCellInWorksheet("A", rowIndex++, worksheetPart);
                cell.CellValue = new CellValue((index).ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }
        }

        public void AddDataColumn(string columnHeader, double[] data)
        {
            // get next column
            string columnName = (dataColumn++).ToString();

            // add column header cell
            addColumnHeader(columnName, columnHeader);

            // add data cells
            uint numRows = (uint)data.Length;
            int j = 0;

            for (uint index = 2U; index < (numRows + 2); index++) {
                Cell cell = insertCellInWorksheet(columnName, index, worksheetPart);
                double val = data[j++];// ?? 0D;
                cell.CellValue = new CellValue(val.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
            }
            worksheetPart.Worksheet.Save();
        }

        private void addColumnHeader(string columnName, string header)
        {
            // add column heading cell
            int index = insertSharedStringItem(header);
            Cell cell = insertCellInWorksheet(columnName, 1U, worksheetPart);
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

        private void createSpreadsheet(Stream stream, string sheetName)
        {
            spreadsheetDoc = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDoc.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDoc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = spreadsheetDoc.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = sheetName };
            sheets.Append(sheet);

            workbookpart.Workbook.Save();
        }

        public void Close()
        {
            spreadsheetDoc.Close();
        }

        private Cell insertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
            // If the cell already exists, returns it. 

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

        private SharedStringTablePart ShareStringPart
        {
            get
            {
                // Get the SharedStringTablePart. If it does not exist, create a new one.
                if (shareStringPart == null) {
                    if (spreadsheetDoc.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0) {
                        shareStringPart = spreadsheetDoc.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    } else {
                        shareStringPart = spreadsheetDoc.WorkbookPart.AddNewPart<SharedStringTablePart>();
                    }
                }
                return shareStringPart;
            }
        }

        private int insertSharedStringItem(string text)
        {
            // If the part does not contain a SharedStringTable, create it.
            if (ShareStringPart.SharedStringTable == null) {
                ShareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;
            foreach (SharedStringItem item in ShareStringPart.SharedStringTable.Elements<SharedStringItem>()) {
                if (item.InnerText == text) {
                    // The text already exists in the part. Return its index.
                    return i;
                }
                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem.
            ShareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            ShareStringPart.SharedStringTable.Save();

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
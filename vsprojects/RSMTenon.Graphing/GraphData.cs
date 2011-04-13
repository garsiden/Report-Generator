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
        public Stream BinaryStream { get; private set; }
        public string ExternalDataId { get; private set; }
        public static readonly string SHEETNAME = "Sheet1";

        private SpreadsheetDocument spreadsheetDoc;
        private WorksheetPart worksheetPart;
        private SharedStringTablePart shareStringPart;
        private char dataColumn = 'B';
        private static readonly string textColumn = "A";

        public GraphData(string externalDataId)
        {
            ExternalDataId = externalDataId;
            BinaryStream = new MemoryStream();
            createSpreadsheet(BinaryStream, GraphData.SHEETNAME);
        }

        public string AddTextColumn(string columnHeader, string[] text)
        {
            Cell cell;
            int stringIndex;
            uint rowIndex = 2U;

            // add column header cell
            addColumnHeader(TextColumn, columnHeader);

            foreach (var item in text) {
                // add string to shared string table, creating table if required
                stringIndex = insertSharedStringItem(item);
                cell = insertCellInWorksheet(TextColumn, rowIndex++, worksheetPart);
                cell.CellValue = new CellValue(stringIndex.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }

            worksheetPart.Worksheet.Save();
            return TextColumn;
        }

        public string AddTextColumn(string columnHeader, IEnumerable<string> text)
        {
            Cell cell;
            int stringIndex;
            uint rowIndex = 2U;

            // add column header cell
            addColumnHeader(TextColumn, columnHeader);

            foreach (var item in text) {
                // add string to shared string table, creating table if required
                stringIndex = insertSharedStringItem(item);
                cell = insertCellInWorksheet(TextColumn, rowIndex++, worksheetPart);
                cell.CellValue = new CellValue(stringIndex.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }

            worksheetPart.Worksheet.Save();
            return TextColumn;
        }

        public string AddDateColumn(string columnHeader, int[] dates)
        {
            Cell cell;
            uint rowIndex = 2U;

            // add column header cell
            addColumnHeader(DateColumn, columnHeader);

            foreach (int date in dates) {
                cell = insertCellInWorksheet(DateColumn, rowIndex++, worksheetPart);
                CellValue cellValue = new CellValue();
                cellValue.Text = date.ToString();
                cell.Append(cellValue);
            }

            worksheetPart.Worksheet.Save();
            return DateColumn;
        }

        public string AddDataColumn(string columnHeader, double[] data)
        {
            // get next column
            string columnName = DataColumn;

            // add column header cell
            addColumnHeader(columnName, columnHeader);

            // add data cells
            Cell cell;
            uint rowIndex = 2U;
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();

            foreach (double value in data) {
                cell = insertCellInWorksheet(columnName, rowIndex++, worksheetPart);
                cell.CellValue = new CellValue(value.ToString());
                cell.DataType = CellValues.Number;
            }

            worksheetPart.Worksheet.Save();
            dataColumn++;

            return columnName;
        }

        private Cell addColumnHeader(string columnName, string header)
        {
            // add column heading cell
            int index = insertSharedStringItem(header);
            Cell cell = insertCellInWorksheet(columnName, 1U, worksheetPart);
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.CellValue = new CellValue() { Text = index.ToString() };

            return cell;
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

                //worksheet.Save();
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

        public string DataColumn { get { return dataColumn.ToString(); } }

        public string TextColumn { get { return textColumn; } }

        public string DateColumn { get { return textColumn; } }

        ~GraphData()
        {
            if (BinaryStream != null)
                BinaryStream.Close();
        }

        // WIP methods
        public void AddTextSeries(string[] headers, IEnumerable<TextSeries> series)
        {
            Row r = createHeaderRow(headers);
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            sheetData.AppendChild(r);

            int rowIndex = 2;

            foreach (var s in series) {
                r = createContentRow(rowIndex++, s.Name, s.Values);
                sheetData.AppendChild(r);
            }

            worksheet.Save();
        }

        private Row createHeaderRow(string[] headers)
        {
            Cell c;
            Row r = new Row() { RowIndex = 1 };
            char col = 'A';

            foreach (var h in headers) {
                c = createTextCell((col++).ToString(), h, 1);
                r.AppendChild(c);
            }

            return r;
        }

        private Row createContentRow(int index, string firstColVal, IEnumerable<double>values) 
        {
            Row r = new Row();
            r.RowIndex = (UInt32)index;
            char col = 'B';

            Cell cell = createTextCell("A", firstColVal, index);
            r.AppendChild(cell);

            //Create cells that contain data
            foreach (double val in values) {
                Cell c = new Cell();
                c.CellReference = col.ToString() + index;
                CellValue v = new CellValue();
                v.Text = val.ToString();

                c.AppendChild(v);
                r.AppendChild(c);
            }
            return r;
        }

        private Cell createTextCell(string header, string text, int index)
        {
            //Create new inline string cell 
            Cell c = new Cell();
            c.DataType = CellValues.InlineString;
            c.CellReference = header + index;

            //Add text to text cell 
            InlineString inlineString = new InlineString();
            Text t = new Text();
            t.Text = text;
            inlineString.AppendChild(t);
            c.AppendChild(inlineString);

            return c;
        }
    }
}
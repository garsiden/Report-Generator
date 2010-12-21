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
    public class CellProps
    {
        public JustificationValues align = JustificationValues.Right;
        public string text = null;
        public int span = 1;
        public bool boxed = false;
    }

    class ModelTable
    {
        private string[] colwidths;

        public Table GenerateTable(string[] colwidths, string[] headers)
        {
            this.colwidths = colwidths;
            Table table1 = new Table();


            TableProperties tableProperties1 = new TableProperties();
            TableWidth tableWidth1 = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Dxa };
            TableIndentation tableIndentation1 = new TableIndentation() { Width = 108, Type = TableWidthUnitValues.Dxa };
            TableLook tableLook1 = new TableLook() { Val = "04A0" };

            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableIndentation1);
            tableProperties1.Append(tableLook1);

            TableGrid tableGrid1 = new TableGrid();
            GridColumn gridColumn;
            
            foreach (string cw in colwidths) {
                gridColumn = new GridColumn() { Width = cw };
                tableGrid1.Append(gridColumn);
            }

            TableRow headerRow = generateHeaderRow(headers);
            table1.Append(headerRow);

            return table1;
        }

        // Creates an TableRowProperties instance and adds its children.
        private TableRowProperties generateTableRowProperties(UInt32Value height)
        {
            TableRowProperties tableRowProperties1 = new TableRowProperties();
            TableRowHeight tableRowHeight1 = new TableRowHeight() { Val = (UInt32Value)height };

            tableRowProperties1.Append(tableRowHeight1);
            return tableRowProperties1;
        }


        public TableRow GenerateTableHeaderRow( List<CellProps> cellProps, UInt32Value height)
        {
            TableRow tableRow1 = new TableRow();
            TableRowProperties tableRowProperties1 = generateTableRowProperties(height);
            tableRow1.Append(tableRowProperties1);

            foreach (CellProps cp in cellProps) {
                TableCell tableCell1 = new TableCell();
                TableCellProperties tableCellProperties1 = new TableCellProperties();
                TableCellBorders tableCellBorders1 = generateTableCellBordersPlain();

                Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };
                TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                HideMark hideMark1 = new HideMark();

                tableCellProperties1.Append(tableCellBorders1);
                tableCellProperties1.Append(shading1);
                tableCellProperties1.Append(tableCellVerticalAlignment1);
                tableCellProperties1.Append(hideMark1);

                Paragraph paragraph1 = new Paragraph();

                ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                Justification justification1 = new Justification() { Val = cp.align };

                ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
                Bold bold1 = new Bold();
                BoldComplexScript boldComplexScript1 = new BoldComplexScript();
                Color color1 = new Color() { Val = "333399" };
                FontSize fontSize1 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "18" };

                paragraphMarkRunProperties1.Append(bold1);
                paragraphMarkRunProperties1.Append(boldComplexScript1);
                paragraphMarkRunProperties1.Append(color1);
                paragraphMarkRunProperties1.Append(fontSize1);
                paragraphMarkRunProperties1.Append(fontSizeComplexScript1);

                paragraphProperties1.Append(justification1);
                paragraphProperties1.Append(paragraphMarkRunProperties1);

                Run run1 = new Run();

                RunProperties runProperties1 = new RunProperties();
                Bold bold2 = new Bold();
                BoldComplexScript boldComplexScript2 = new BoldComplexScript();
                Color color2 = new Color() { Val = "333399" };
                FontSize fontSize2 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "18" };

                runProperties1.Append(bold2);
                runProperties1.Append(boldComplexScript2);
                runProperties1.Append(color2);
                runProperties1.Append(fontSize2);
                runProperties1.Append(fontSizeComplexScript2);
                run1.Append(runProperties1);

                if (cp.text != null) {
                    Text text1 = new Text();
                    text1.Text = cp.text;
                    run1.Append(text1);
                }

                paragraph1.Append(paragraphProperties1);
                paragraph1.Append(run1);

                tableCell1.Append(tableCellProperties1);
                tableCell1.Append(paragraph1);
                tableRow1.Append(tableCell1);
            }

            return tableRow1;
        }

        public TableRow GenerateTableRow(List<CellProps> cellProps, UInt32Value height)
        {
            TableRow tableRow1 = new TableRow();
            TableRowProperties tableRowProperties1 = generateTableRowProperties(height);
            tableRow1.Append(tableRowProperties1);

            foreach (CellProps cp in cellProps) {
                if (cp.span == 0) continue;
                TableCell tableCell1 = new TableCell();
                TableCellProperties tableCellProperties1 = new TableCellProperties();
                TableCellBorders tableCellBorders1 = null;
                if (cp.boxed) {
                    tableCellBorders1 = generateTableCellBordersBox();
                } else {
                    tableCellBorders1 = generateTableCellBordersPlain();
                }
                Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };
                NoWrap noWrap1 = new NoWrap();
                TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                HideMark hideMark1 = new HideMark();

                if (cp.span > 1) {
                    GridSpan span = new GridSpan() { Val = (Int32Value)cp.span };
                    tableCellProperties1.Append(span);
                }

                tableCellProperties1.Append(tableCellBorders1);
                tableCellProperties1.Append(shading1);
                tableCellProperties1.Append(noWrap1);
                tableCellProperties1.Append(tableCellVerticalAlignment1);
                tableCellProperties1.Append(hideMark1);

                Paragraph paragraph1 = new Paragraph();

                ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                Justification justification1 = new Justification() { Val = cp.align };

                ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
                FontSize fontSize1 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "18" };

                paragraphMarkRunProperties1.Append(fontSize1);
                paragraphMarkRunProperties1.Append(fontSizeComplexScript1);

                paragraphProperties1.Append(justification1);
                paragraphProperties1.Append(paragraphMarkRunProperties1);

                Run run1 = new Run();

                RunProperties runProperties1 = new RunProperties();
                FontSize fontSize2 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "18" };

                runProperties1.Append(fontSize2);
                runProperties1.Append(fontSizeComplexScript2);

                run1.Append(runProperties1);
                if (cp.text != null) {
                    Text text1 = new Text();
                    text1.Text = cp.text;
                    run1.Append(text1);
                }

                paragraph1.Append(paragraphProperties1);
                paragraph1.Append(run1);

                tableCell1.Append(tableCellProperties1);
                tableCell1.Append(paragraph1);
                tableRow1.Append(tableCell1);

            }

            return tableRow1;
        }

        public TableRow GenerateTableFooterRow(List<CellProps> cellProps, UInt32Value height)
        {
            // Add table row properties
            TableRow tableRow1 = new TableRow();
            TableRowProperties tableRowProperties1 = generateTableRowProperties(height);
            tableRow1.Append(tableRowProperties1);

            foreach (CellProps cp in cellProps) {
                if (cp.span == 0) continue;
                TableCell tableCell1 = new TableCell();
                TableCellProperties tableCellProperties1 = new TableCellProperties();
                TableCellBorders tableCellBorders1;

                if (cp.boxed) {
                    tableCellBorders1 = generateTableCellBordersBox();
                } else {
                    tableCellBorders1 = generateTableCellBordersPlain();
                }

                Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };
                NoWrap noWrap1 = new NoWrap();
                TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                HideMark hideMark1 = new HideMark();

                tableCellProperties1.Append(tableCellBorders1);
                tableCellProperties1.Append(shading1);
                tableCellProperties1.Append(noWrap1);
                tableCellProperties1.Append(tableCellVerticalAlignment1);
                tableCellProperties1.Append(hideMark1);

                Paragraph paragraph1 = new Paragraph();

                ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                Justification justification1 = new Justification() { Val = JustificationValues.Right };

                ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
                Bold bold1 = new Bold();
                BoldComplexScript boldComplexScript1 = new BoldComplexScript();
                Color color1 = new Color() { Val = "333399" };
                FontSize fontSize1 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "18" };

                paragraphMarkRunProperties1.Append(bold1);
                paragraphMarkRunProperties1.Append(boldComplexScript1);
                paragraphMarkRunProperties1.Append(color1);
                paragraphMarkRunProperties1.Append(fontSize1);
                paragraphMarkRunProperties1.Append(fontSizeComplexScript1);

                paragraphProperties1.Append(justification1);
                paragraphProperties1.Append(paragraphMarkRunProperties1);

                Run run1 = new Run();

                RunProperties runProperties1 = new RunProperties();
                Bold bold2 = new Bold();
                BoldComplexScript boldComplexScript2 = new BoldComplexScript();
                Color color2 = new Color() { Val = "333399" };
                FontSize fontSize2 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "18" };

                runProperties1.Append(bold2);
                runProperties1.Append(boldComplexScript2);
                runProperties1.Append(color2);
                runProperties1.Append(fontSize2);
                runProperties1.Append(fontSizeComplexScript2);
                run1.Append(runProperties1);

                if (cp.text != null) {
                    Text text1 = new Text();
                    text1.Text = cp.text;
                    run1.Append(text1);
                }

                paragraph1.Append(paragraphProperties1);
                paragraph1.Append(run1);

                tableCell1.Append(tableCellProperties1);
                tableCell1.Append(paragraph1);
                tableRow1.Append(tableCell1);                
            }
            return tableRow1;
        }

        private TableCellBorders generateTableCellBordersPlain()
        {
            TableCellBorders tableCellBorders1 = new TableCellBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Nil };
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Nil };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders1.Append(topBorder1);
            tableCellBorders1.Append(leftBorder1);
            tableCellBorders1.Append(bottomBorder1);
            tableCellBorders1.Append(rightBorder1);

            return tableCellBorders1;
        }

        // Creates an TableCellBorders instance and adds its children.
        private TableCellBorders generateTableCellBordersBox()
        {
            TableCellBorders tableCellBorders1 = new TableCellBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders1.Append(topBorder1);
            tableCellBorders1.Append(leftBorder1);
            tableCellBorders1.Append(bottomBorder1);
            tableCellBorders1.Append(rightBorder1);

            return tableCellBorders1;
        }


        private TableRow generateHeaderRow(string[] headers)
        {
            // create a header row
            var cellProps = new List<CellProps>();

            cellProps.Add(new CellProps() { text = headers[0], align = JustificationValues.Left });
            cellProps.Add(new CellProps() { text = headers[1] });
            cellProps.Add(new CellProps() { text = headers[2] });
            cellProps.Add(new CellProps() { text = headers[3] });
            cellProps.Add(new CellProps() { text = headers[4] });
            cellProps.Add(new CellProps() { text = headers[5] });

            TableRow header = GenerateTableHeaderRow(cellProps, 465U);

            return header;

        }
    }
}

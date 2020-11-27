using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsPdfService
{
    public class BorderedTable : IPDFTable
    {
        private string mainFont { get; set; } = "Arial";

        public Table table { get; set; } = new Aspose.Pdf.Table();

        public BorderedTable()
        {
            table.Border = new BorderInfo(Aspose.Pdf.BorderSide.Box, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
            table.DefaultCellBorder = new BorderInfo(Aspose.Pdf.BorderSide.None, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray));

            MarginInfo margin = new Aspose.Pdf.MarginInfo();
            margin.Top = 3f;
            margin.Left = 7f;
            margin.Right = 5f;
            margin.Bottom = 3f;
            table.DefaultCellPadding = margin;
            TextState txtState = new TextState();
            txtState.Font = FontRepository.FindFont(mainFont);
            txtState.FontSize = 10;
            table.DefaultCellTextState = txtState;

            table.ColumnWidths = "20% 80%";          
        }

        public BorderedTable(string columnWidth)
        {
            table.Border = new BorderInfo(Aspose.Pdf.BorderSide.Box, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
            table.DefaultCellBorder = new BorderInfo(Aspose.Pdf.BorderSide.None, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray));

            MarginInfo margin = new Aspose.Pdf.MarginInfo();
            margin.Top = 3f;
            margin.Left = 7f;
            margin.Right = 5f;
            margin.Bottom = 3f;
            table.DefaultCellPadding = margin;
            table.VerticalAlignment = VerticalAlignment.Top;
            TextState txtState = new TextState();
            txtState.Font = FontRepository.FindFont(mainFont);
            txtState.FontSize = 10;
            table.DefaultCellTextState = txtState;

            table.ColumnWidths = columnWidth;
        }
        public Row AddRow (string Caption, string Value)
        {
            var val = Value == null ? string.Empty : Value;

            var row = table.Rows.Add();
            var xcell = row.Cells.Add(Caption);
            xcell = row.Cells.Add(val);

            return row;
        }

        public Row AddRow(Dictionary<string, string> row)
        {
            return null;
        }

    }
}
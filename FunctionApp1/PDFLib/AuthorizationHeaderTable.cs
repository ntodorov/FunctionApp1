using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Generic;

namespace wsPdfService
{
    public class AuthorizationHeaderTable : IPDFTable
    {
        protected string mainFont { get; set; } = "Arial";

        public Table table { get; set; } = new Aspose.Pdf.Table();
        public Cell titleCell { get; set; }

        public int DefaultCollSpan { get; set; } = 2;

        private BorderInfo bottomBorder { get; set; }


        public AuthorizationHeaderTable()
        {
            bottomBorder = new BorderInfo(Aspose.Pdf.BorderSide.Bottom, .1f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));

            Aspose.Pdf.MarginInfo tableMargin = new Aspose.Pdf.MarginInfo();
            tableMargin.Top = 7f;
            //tableMargin.Left = 7f;
            //tableMargin.Right = 5f;
            tableMargin.Bottom = 1f;
            table.Margin = tableMargin;

            table.Border = new BorderInfo(Aspose.Pdf.BorderSide.None, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
            table.DefaultCellBorder = new BorderInfo(Aspose.Pdf.BorderSide.None, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray));

            TextState txtState = new TextState();
            txtState.Font = FontRepository.FindFont(mainFont);
            txtState.FontSize = 10;
            table.DefaultCellTextState = txtState;

            Aspose.Pdf.MarginInfo margin = new Aspose.Pdf.MarginInfo();
            margin.Top = 12f;
            margin.Left = 7f;
            margin.Right = 5f;
            margin.Bottom = 1f;
            table.DefaultCellPadding = margin;

            table.ColumnWidths = "30% 70%";

        }

       
        public void AddTitle(string title)
        {

            if (string.IsNullOrEmpty(title)) return;

            var row = table.Rows.Add();
            titleCell = row.Cells.Add(title);
            titleCell.ColSpan = DefaultCollSpan;

            TextState txtState1 = new TextState();
            txtState1.Font = FontRepository.FindFont(mainFont);
            txtState1.FontSize = 14;


            txtState1.FontStyle = FontStyles.Bold;
            titleCell.DefaultCellTextState = txtState1;
            titleCell.Alignment = HorizontalAlignment.Center;

            Aspose.Pdf.MarginInfo titleMargin = new Aspose.Pdf.MarginInfo();
            titleMargin.Top = 3f;
            titleMargin.Left = 3f;
            titleMargin.Right = 5f;
            titleMargin.Bottom = 1f;
            titleCell.Margin = titleMargin;
        }

        public Row AddRow (string Caption, string Value)
        {
            var normalizedValue = Value == null ? string.Empty : Value;

            var row = table.Rows.Add();
            var xcell = row.Cells.Add(Caption);

            xcell = row.Cells.Add(normalizedValue);
            xcell.Border = bottomBorder;

            return row;
        }

        public Row AddRow(Dictionary<string, string> row)
        {
            return null;
        }


    }
}
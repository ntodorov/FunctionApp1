using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Generic;


namespace wsPdfService
{
    public class VarianceTable : IPDFTable
    {
        protected string mainFont { get; set; } = "Arial";

        public Table table { get; set; } = new Aspose.Pdf.Table();
        public Cell titleCell { get; set; }

        public int DefaultCollSpan { get; set; } = 2;

        protected string title { get; set; }

        public VarianceTable()
        {
            Aspose.Pdf.MarginInfo tableMargin = new Aspose.Pdf.MarginInfo();
            tableMargin.Top = 7f;
            //tableMargin.Left = 7f;
            //tableMargin.Right = 5f;
            tableMargin.Bottom = 1f;
            table.Margin = tableMargin;

            SetTableNoBorder();
  
            TextState txtState = new TextState();
            txtState.Font = FontRepository.FindFont(mainFont);
            txtState.FontSize = 10;
            table.DefaultCellTextState = txtState;

            Aspose.Pdf.MarginInfo margin = new Aspose.Pdf.MarginInfo();
            margin.Top = 5f;
            margin.Left = 5f;
            margin.Right = 5f;
            margin.Bottom = 5f;
            table.DefaultCellPadding = margin;

            table.ColumnWidths = "20% 80%";

        }

        public void AddTitle(string title)
        {
            this.title = title;

            if ((title == null) || (title == string.Empty)) return;

            var row = table.Rows.Add();
            titleCell = row.Cells.Add(title);
            titleCell.ColSpan = DefaultCollSpan;

            TextState txtState1 = new TextState();
            txtState1.Font = FontRepository.FindFont(mainFont);
            txtState1.FontSize = 11;
            txtState1.ForegroundColor = Color.White;
            txtState1.BackgroundColor = Color.DarkBlue;
            txtState1.FontStyle = FontStyles.Bold;
            titleCell.DefaultCellTextState = txtState1;

            titleCell.BackgroundColor = Color.DarkBlue;
            Aspose.Pdf.MarginInfo titleMargin = new Aspose.Pdf.MarginInfo();
            titleMargin.Top = 3f;
            titleMargin.Left = 3f;
            titleMargin.Right = 5f;
            titleMargin.Bottom = 1f;
            titleCell.Margin = titleMargin;
        }

        protected void SetTableNoBorder()
        {
            TextState txtState = new TextState();
            txtState.Font = FontRepository.FindFont(mainFont);
            txtState.FontSize = 7;
            table.DefaultCellTextState = txtState;

            table.Border = new BorderInfo(Aspose.Pdf.BorderSide.None, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
            table.DefaultCellBorder = new BorderInfo(Aspose.Pdf.BorderSide.None, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray));
            Aspose.Pdf.MarginInfo margin = new Aspose.Pdf.MarginInfo();

        }

        public Row AddRow (string Caption, string Value)
        {
            var val = Value == null ? string.Empty : Value;

            var row = table.Rows.Add();
            row.DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray));

            //caption
            var xcell = row.Cells.Add(Caption);
            xcell.VerticalAlignment = VerticalAlignment.Top;

            //value
            xcell = row.Cells.Add(val);

            return row;
        }

        public Row AddRow(Dictionary<string, string> row)
        {
            return null;
        }


        public Row AddHeadingRow(string Heading)
        {
            var row = table.Rows.Add();
            var xcell = row.Cells.Add(Heading);
            xcell.ColSpan = DefaultCollSpan;

            TextState txtState1 = new TextState();
            txtState1.Font = FontRepository.FindFont(mainFont);
            txtState1.FontSize = 10;
            txtState1.FontStyle = FontStyles.Bold;
            xcell.DefaultCellTextState = txtState1;

            Aspose.Pdf.MarginInfo headingMargin = new Aspose.Pdf.MarginInfo();
            headingMargin.Top = 7f;
            headingMargin.Left = 3f;
            headingMargin.Right = 5f;
            headingMargin.Bottom = 1f;
            xcell.Margin = headingMargin;

            return row;
        }


    }
}
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace wsPdfService
{
    public class PDFGrid //: IPDFTable
    {
        protected string mainFont { get; set; } = "Arial";

        public Table table { get; set; } = new Aspose.Pdf.Table();
        public Cell titleCell { get; set; }

        public int DefaultCollSpan { get; set; } = 2;

        public BorderSide TableBorderSide { get; set; } = BorderSide.All;
        public BorderSide DeafaultCellBorderSide { get; set; } = BorderSide.All;

        private HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Left;

        protected string title { get; set; }
        public bool ColoredTitle { get; set; } = true;

        public List<PDFColumn> Columns { get; set; } = new List<PDFColumn>();

        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Center;

        public PDFGrid(bool noBorder = false, HorizontalAlignment ha = HorizontalAlignment.Left)
        {
            this.horizontalAlignment = ha;

            Aspose.Pdf.MarginInfo tableMargin = new Aspose.Pdf.MarginInfo();
            tableMargin.Top = 7f;
            //tableMargin.Left = 7f;
            //tableMargin.Right = 5f;
            tableMargin.Bottom = 1f;
            table.Margin = tableMargin;


            TextState txtState = new TextState();
            txtState.Font = FontRepository.FindFont(mainFont);
            txtState.FontSize = 7;
            table.DefaultCellTextState = txtState;

            if (noBorder)
            {
                TableBorderSide = BorderSide.None;
                DeafaultCellBorderSide = BorderSide.None;
            }

            table.Border = new Aspose.Pdf.BorderInfo(TableBorderSide, .1f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
            table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(DeafaultCellBorderSide, .1f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray));
            Aspose.Pdf.MarginInfo margin = new Aspose.Pdf.MarginInfo();
            margin.Top = 1f;
            margin.Left = 1f;
            margin.Right = 3f;
            margin.Bottom = 1f;
            table.DefaultCellPadding = margin;

        }

        private void PopulateColumnWidths()
        {
            table.ColumnWidths = String.Join(" ", Columns.Select(c => c.WidthPercent));
        }

        public PDFColumn DefineColumn(string title, string widthPercent, string tibcoName)
        {            
            return DefineColumn(title, widthPercent, tibcoName, String.Empty);
        }

        public PDFColumn DefineColumn(string title, string widthPercent, string tibcoName, string defaultValue)
        {
            var newCol = new PDFColumn()
            {
                Title = title,
                WidthPercent = widthPercent,
                TibcoName = tibcoName,
                DefaultValue = defaultValue
            };

            Columns.Add(newCol);
            PopulateColumnWidths();

            return newCol;
        }

        public PDFColumn DefineColumn(string title, string widthPercent)
        {
            var tibcoName = title.ToUpper();

            return DefineColumn(title, widthPercent, tibcoName, String.Empty);
        }

        //in this class it is used only for adding empty row. 
        public Row AddRow(string Caption, string Value)
        {
            var r = table.Rows.Add();

            foreach (var col in Columns) r.Cells.Add("");

            if (r.Cells.Count > 0)
            {
                r.Cells[0].Paragraphs.Clear();
                r.Cells[0].Paragraphs.Add(new TextFragment(Caption));
               // r.Cells[0].ColSpan = r.Cells.Count;
            }

            return r;
        }

        public Row AddRow(Dictionary<string, string> row)
        {
            var r = table.Rows.Add();

            foreach(var col in Columns)
            {
                var value = row.ContainsKey(col.TibcoName) ? row[col.TibcoName] : col.DefaultValue;
                value = value ?? string.Empty;
                var xcell = r.Cells.Add(value);
                xcell.Alignment =this.horizontalAlignment;
                xcell.VerticalAlignment = this.verticalAlignment;
            }

            return r;
        }
       
        public void AddRows(List<Dictionary<string, string>> rows)
        {
            foreach(var r in rows)
            {
                AddRow(r);
            }

            if (rows?.Count == 0) AddRow("", "");

        }


        public Row AddTitleRow()
        {
            Cell xcell;
            var row = table.Rows.Add();
            foreach (var c in Columns)
            {
                xcell = row.Cells.Add(c.Title);
                if (ColoredTitle)
                    xcell.BackgroundColor = Color.LightGray;
                else
                    xcell.DefaultCellTextState.FontStyle = FontStyles.Bold;

                xcell.Alignment = horizontalAlignment;
            }

            return row;
        }

        public class PDFColumn
        {
            public string Title { get; set; }
            public string WidthPercent { get; set; }

            public string TibcoName { get; set; }

            public string DefaultValue { get; set; }
        }

       
    }
}
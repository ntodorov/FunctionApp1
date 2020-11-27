using Aspose.Pdf;


namespace wsPdfService
{
    public class FourColsSectionTable : SectionTable
    {
        private BorderInfo uBorder { get; set; } = new BorderInfo(Aspose.Pdf.BorderSide.Bottom, .3f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));

        public FourColsSectionTable()
        {
            table.ColumnWidths = "20% 36% 2% 10% 32% 2%";
        }


        public Row AddRow (string Caption1, string Value1, string Caption2, string Value2, bool underlineValue1 = true, bool underlineValue2 = true)
        {
            var val1 = Value1 == null ? string.Empty : Value1;
            var val2 = Value2 == null ? string.Empty : Value2;

            var row = table.Rows.Add();

            var xcell = row.Cells.Add(Caption1);
            xcell = row.Cells.Add(val1);
            if (underlineValue1) xcell.Border = uBorder;

            row.Cells.Add("");

            xcell = row.Cells.Add(Caption2);
            xcell = row.Cells.Add(val2);
            if (underlineValue2) xcell.Border = uBorder;

            return row;
        }

        public Row AddRow2(string Caption1, string Value1, string Caption2, string Value2, bool underlineValue1 = true, bool underlineValue2 = true)
        {
            var val1 = Value1 == null ? string.Empty : Value1;
            var val2 = Value2 == null ? string.Empty : Value2;

            var row = table.Rows.Add();

            var xcell = row.Cells.Add(Caption1); xcell.VerticalAlignment = VerticalAlignment.Top;
            xcell = row.Cells.Add(val1); xcell.VerticalAlignment = VerticalAlignment.Top;
            if (underlineValue1) xcell.Border = uBorder;

            row.Cells.Add("");

            xcell = row.Cells.Add(Caption2); xcell.VerticalAlignment = VerticalAlignment.Top;
            xcell = row.Cells.Add(val2); xcell.VerticalAlignment = VerticalAlignment.Top;
            if (underlineValue2) xcell.Border = uBorder;

            return row;
        }
    }
}
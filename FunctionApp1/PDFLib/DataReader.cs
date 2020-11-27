using Aspose.Pdf;
using System;
using System.Collections.Generic;

namespace wsPdfService
{
    public class DataReader
    {
        public Dictionary<string, string> DataSource { get; set; }
        public IPDFTable PDFTable { get; set; }

        public Row AddDataRow(string Caption, string ColumnName, string AnswerType = "")
        {
            if (PDFTable == null) throw new Exception("PDFTable cannot be null!");

            //even without datasource we want to show the captions with empty values
            if (DataSource == null) DataSource = new Dictionary<string, string>();

            string value = DataSource.ContainsKey(ColumnName) ? DataSource[ColumnName] : string.Empty;
            value = value == null ? string.Empty : value;
            //if UI put \n only, replace with \r\n
            if (value.Contains("\n") && !value.Contains("\r\n"))
                  value = value.Replace("\n", "\r\n");
         


            if (AnswerType == "checkbox") value = this.checkBoxAnswer(value);
            if (AnswerType == "pla_dateFormat") value = this.ReturnFormattedDate(value);  // first used in PLA 

            return PDFTable.AddRow(Caption, value);
        }


        public Row AddDataRow(string Caption)
        {
            return AddDataRow(Caption, Caption.ToUpper());
        }

        public bool Checked(string ColumnName)
        {
            //even without datasource we want to show the captions with empty values
            if (DataSource == null) DataSource = new Dictionary<string, string>();
            
            return DataSource.ContainsKey(ColumnName) && DataSource[ColumnName] == "Y";
        }

        private string checkBoxAnswer(string answer)
        {
            string checkBoxChecked = ((char)0x2611).ToString();
            string checkBoxUnchecked = ((char)0x2610).ToString();

            string retval = checkBoxUnchecked + " Yes  " + checkBoxUnchecked + " No";
            switch (answer.ToUpper())
            {
                case "Y":
                    retval = checkBoxChecked + " Yes  " + checkBoxUnchecked + " No";
                    break;
                case "N":
                    retval = checkBoxUnchecked + " Yes  " + checkBoxChecked + " No";
                    break;
            }

            return retval;
        }

        private string ReturnFormattedDate(string iDate, string dateFormat = "MM/dd/yyyy")
        {
            string retval = "";
            if (!string.IsNullOrEmpty(iDate))
            {

                try
                {
                    retval = Convert.ToDateTime(iDate).ToString(dateFormat);
                }
                catch
                {
                    retval = iDate;  //return original value
                }

            }
            return retval;
        }
    }
}
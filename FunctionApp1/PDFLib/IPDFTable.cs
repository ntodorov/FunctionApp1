using Aspose.Pdf;
using System.Collections.Generic;


namespace wsPdfService
{
    public interface IPDFTable
    {
        Row AddRow(string Caption, string Value);
        Row AddRow(Dictionary<string, string> row);
    }
}

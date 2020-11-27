using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using wsPdfService;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            Aspose.Pdf.Document doc = new Aspose.Pdf.Document();
            var page = doc.Pages.Add();

            WebClient webClient = new WebClient();
            byte[] data1 = webClient.DownloadData("https://i.imgur.com/UDbyrwG.jpg");
            MemoryStream memStream = new MemoryStream(data1);

            var image = new Aspose.Pdf.Image();
            image.ImageStream = memStream;


            //string outFile = "c:\\temp\\__ImagetoPDF.pdf";

            // Set margins so image will fit, etc.

            page.PageInfo.Margin.Bottom = 0;
            page.PageInfo.Margin.Top = 0;
            page.PageInfo.Margin.Left = 0;
            page.PageInfo.Margin.Right = 0;
            page.PageInfo.Height = Aspose.Pdf.PageSize.A4.Height;
            page.PageInfo.Width = Aspose.Pdf.PageSize.A4.Width;

            //Add the image into paragraphs collection of the section
            page.Paragraphs.Add(image);


            //Lets add new page and put table with data in it.
            page = doc.Pages.Add();
            var table = GeneralAERApplication();
            page.Paragraphs.Add(table);
                       

            //save to Azure Storage??
            //doc.Save(outFile);

            //currently for the test I will convert it to base64str
            using (var stream = new MemoryStream())
            {
                doc.Save(stream);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();

                responseMessage = Convert.ToBase64String(bytes);
            }


            return new OkObjectResult(responseMessage);
        }

        private static Dictionary<string, string> mockData1 = new Dictionary<string, string>();
        private static Dictionary<string, string> mockData2 = new Dictionary<string, string>();

        private static Aspose.Pdf.Table GeneralAERApplication()
        {
            GenerateMockData();

            var reader = new DataReader(); //You may need to create your own data reader version, depending on the data types you will have. This one is Dictionary base, because it was result from XML (SOAP) tranfomed response.
            var section = new SectionTable();
            section.AddTitle("GENERAL APPLICATION");

            var btable = new BorderedTable();
            reader.PDFTable = btable;
            reader.DataSource = mockData1;
            reader.AddDataRow("Application Type", "APPLICATION TYPE");
            reader.AddDataRow("Activity Type", "APPLICATION PURPOSE");
            reader.AddDataRow("Activity Description", "PROJECT DESCRIPTION");
            section.AddPDFTable(btable.table);

            //add location grid
            //var row = section.AddRow("Activity Location (LLD)", "");
            //var pdfGrid = new PDFGrid();
            //pdfGrid.DefineColumn("Quarter", "20%", "QUARTER");
            //pdfGrid.DefineColumn("SEC", "20%", "SECTION");
            //pdfGrid.DefineColumn("Township", "20%", "TOWNSHIP");
            //pdfGrid.DefineColumn("Range", "20%", "RANGE");
            //pdfGrid.DefineColumn("Meridian", "20%", "MERIDIAN");
            //pdfGrid.AddTitleRow();
            //pdfGrid.AddRows(activityInfo.LLDGrid.Rows);
            //var paragraphs = row.Cells[1].Paragraphs;
            //paragraphs.Clear();
            //paragraphs.Add(pdfGrid.table);


            //changing the datasource
            reader.DataSource = mockData2;
            reader.PDFTable = section;

            reader.AddDataRow("Disposition number obtained to undertake this activity", "DISPOSITION NUMBER");
            reader.AddDataRow("First Nations Consultation Number (FNC #)", "FIRST NATIONS CONSULTATION NUMBER");
            reader.AddDataRow("ACO Adequacy Status", "AOC ADEQUACY STATUS");
            reader.AddDataRow("Proposed Activity Start Date", "START DATE");
            reader.AddDataRow("Proposed Activity End Date", "END DATE");


            return section.table;
        }

        private static void GenerateMockData()
        {
            mockData1.Clear();
            mockData1.Add("APPLICATION TYPE", "Public Lands");
            mockData1.Add("APPLICATION PURPOSE", "MSL");
            mockData1.Add("PROJECT DESCRIPTION", "Fabricam Co. plans to use the land to extract oil, lorem upsum etc.");

            mockData2.Clear();

            mockData2.Add("DISPOSITION NUMBER", "MSL2345678");
            mockData2.Add("FIRST NATIONS CONSULTATION NUMBER", "123");
            mockData2.Add("AOC ADEQUACY STATUS", "Approved");
            mockData2.Add("START DATE", "2020-01-24");
            mockData2.Add("END DATE", "2022-12-01");


        }
    }
}

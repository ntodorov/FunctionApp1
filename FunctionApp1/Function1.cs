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


            string outFile = "c:\\temp\\__ImagetoPDF.pdf";

            // Set margins so image will fit, etc.

            page.PageInfo.Margin.Bottom = 0;

            page.PageInfo.Margin.Top = 0;

            page.PageInfo.Margin.Left = 0;

            page.PageInfo.Margin.Right = 0;

            page.PageInfo.Height = Aspose.Pdf.PageSize.A4.Height;

            page.PageInfo.Width = Aspose.Pdf.PageSize.A4.Width;

            //Add the image into paragraphs collection of the section

            page.Paragraphs.Add(image);

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
    }
}

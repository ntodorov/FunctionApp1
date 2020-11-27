using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using Aspose.Pdf;

namespace wsPdfService
{
    public class Watermark
    {

        public void PutWatermarkOnDocument(Document pdfDocument, string text, int pageNum = 9999)
        {
            ImageStamp imageStamp = this.CreateWatermarkFromText(text);
            PageCollection pageColection = pdfDocument.Pages;
            if (pageNum == 9999)
            {

                foreach (Page pdfPage in pageColection)
                    pdfPage.AddStamp(imageStamp);
            }
            else
            {
                if (pageColection.Count >= pageNum - 1)
                    pdfDocument.Pages[pageNum].AddStamp(imageStamp);
            }


        }

        public void PutWatermarkOnDocument(Document pdfDocument, ref Bitmap image, int pageNum = 9999)
        {
            ImageStamp imageStamp = this.CreateWatermarkFromImage(image);
            PageCollection pageColection = pdfDocument.Pages;
            if (pageNum == 9999)
            {

                foreach (Page pdfPage in pageColection)
                    pdfPage.AddStamp(imageStamp);
            }
            else
            {
                if (pageColection.Count >= pageNum - 1)
                    pdfDocument.Pages[pageNum].AddStamp(imageStamp);
            }


        }

        public void PutWatermarkOverSideMargins(Document pdfDocument, string stampMsg, int pageNum = 1)
        {

            // Create text stamp
            TextStamp textStamp = new TextStamp(stampMsg);
            // Set whether stamp is background
            textStamp.Background = false;
            // Set text properties
            textStamp.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial");
            textStamp.TextState.FontSize = 10.0F;
            textStamp.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            //textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            const int charWidth = 5;
            if (pdfDocument.Pages.Count >= pageNum)
            {

                var page = pdfDocument.Pages[pageNum];


                //left margin
                textStamp.Rotate = Rotation.on90;
                textStamp.XIndent = 10;
                textStamp.YIndent = (page.PageInfo.Height - (stampMsg.Length * charWidth)) / 2;
                page.AddStamp(textStamp);

                //right margin
                textStamp.Rotate = Rotation.on270;
                textStamp.XIndent = page.PageInfo.Width - 25;
                textStamp.YIndent = (page.PageInfo.Height - (stampMsg.Length * charWidth)) / 2;
                page.AddStamp(textStamp);

                //top
                textStamp.Rotate = Rotation.None;
                textStamp.YIndent = page.PageInfo.Height - 25;
                textStamp.XIndent = (page.PageInfo.Width - (stampMsg.Length * charWidth)) / 2;
                page.AddStamp(textStamp);

                //bottom 
                textStamp.YIndent = 10;
                textStamp.XIndent = (page.PageInfo.Width - (stampMsg.Length * charWidth)) / 2;
                page.AddStamp(textStamp);


            }

        }


        // --------------------------------------------------------------
        // This converts a text to a watermark
        // Text is converted to image then to a watermark tilted by 30 degrees.
        // This is done because Aspose can only tilt a text by 90 degrees
        public ImageStamp CreateWatermarkFromText(string text)
        {
          
            text = string.IsNullOrEmpty(text) ? "text is empty!!" : text;

            double imageHeight = 30;
            Bitmap myImage = CreateBitmapImage(text);
            MemoryStream memoryStream = new MemoryStream();
            myImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            int indentX = 100 - ((text.Length - 30) * 3);
            indentX = text.Length < 30 ? 100 : indentX;

            int imageWidth = 300 + ((text.Length - 30) * 3);
            imageWidth = text.Length < 30 ? 300 : imageWidth;
            imageWidth = imageWidth < 300 ? 300 : imageWidth;
            imageHeight = text.Length < 15 ? 70 : imageHeight;


            // Create image stamp
            ImageStamp imageStamp = new ImageStamp(memoryStream);
            imageStamp.Background = true;
            imageStamp.Quality = 100;
            imageStamp.XIndent = indentX;
            imageStamp.YIndent = 300;
            imageStamp.Height = imageHeight;
            imageStamp.Width = imageWidth;
            imageStamp.Rotate = Rotation.None;
            imageStamp.RotateAngle = 30.0;
            imageStamp.Opacity = 0.1;

            return imageStamp;

        }

        public ImageStamp CreateWatermarkFromImage(Bitmap image, int xIndent = 100, int imageHeight = 200, int imageWidth = 400)
        {
            // Create image stamp
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            ImageStamp imageStamp = new ImageStamp(memoryStream);
            imageStamp.Background = true;
            imageStamp.Quality = 100;
            imageStamp.XIndent = xIndent;
            imageStamp.YIndent = 300;
            imageStamp.Height = imageHeight;
            imageStamp.Width = imageWidth;
            imageStamp.Rotate = Rotation.None;
            imageStamp.RotateAngle = 30.0;
            imageStamp.Opacity = 0.5;

            return imageStamp;
        }

        public Bitmap CreateBitmapImage(string sImageText)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create the Font object for the image text drawing.
            Font objFont = new Font("Arial", 14, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(System.Drawing.Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(System.Drawing.Color.FromArgb(102, 102, 102)), 0, 0);
            objGraphics.Flush();
            return (objBmpImage);
        }


    }


}
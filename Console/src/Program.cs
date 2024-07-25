using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Quality;

namespace HelloWorld
{
    class Program
    {
        /// <summary>
        /// 下記PDFSharpサンプルソースを流用
        /// src/samples/src/PDFsharp/src/HelloWorld/Program.cs
        /// </summary>
        /// <param name="args">パラメータ</param>
        static void Main(string[] args)
        {
            // フォントリゾルバーのグローバル登録
            PdfSharp.Fonts.GlobalFontSettings.FontResolver = new JapaneseFontResolver();

            // Create a new PDF document.
            var document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            document.Info.Subject = "Just a simple Hello-World program.";

            // Create an empty page in this document.
            var page = document.AddPage();
            //page.Size = PageSize.Letter;

            // Get an XGraphics object for drawing on this page.
            var gfx = XGraphics.FromPdfPage(page);

            // Draw two lines with a red default pen.
            var width = page.Width.Point;
            var height = page.Height.Point;
            gfx.DrawLine(XPens.Red, 0, 0, width, height);
            gfx.DrawLine(XPens.Red, width, 0, 0, height);

            // Draw a circle with a red pen which is 1.5 point thick.
            var r = width / 5;
            gfx.DrawEllipse(new XPen(XColors.Red, 1.5), XBrushes.White, new XRect(width / 2 - r, height / 2 - r, 2 * r, 2 * r));

            // Create a font.
            var font = new XFont("Gen Shin Gothic", 20, XFontStyleEx.BoldItalic, new XPdfFontOptions(PdfFontEmbedding.EmbedCompleteFontFile));

            // Draw the text.
            gfx.DrawString("こんにちわ, PDFsharp!", font, XBrushes.Black,
                new XRect(0, 0, width, height), XStringFormats.Center);
    
            // Save the document...
            var fullName = PdfFileUtility.GetTempPdfFullFileName($"HelloWorld_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
            document.Save(fullName);
        }
    }
}
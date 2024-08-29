using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using PdfSharp.Drawing.Layout;

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

            // 日本語フォント設定
            var font = new XFont("Gen Shin Gothic", 20, XFontStyleEx.BoldItalic, new XPdfFontOptions(PdfFontEmbedding.EmbedCompleteFontFile));

            // 枠線設定
            var pen = new XPen(XColors.Black, 3);

            // 矩形設定
            var width = page.Width.Point;
            var height = page.Height.Point;
            var rect = new XRect(100,100,150,100);

            // 矩形描画
            gfx.DrawRectangle(pen, rect);

            // 文字描画
            rect.X += 5;
            rect.Y += 5;
            rect.Height -= 5;
            rect.Width -= 5;
            var tf = new XTextFormatter(gfx);
            tf.DrawString("こんにちわ。\nPDFsharp!", font, XBrushes.Black, rect, XStringFormats.TopLeft);

            // 表形式描画確認
            var singlePen = new XPen(XColors.Gray, 1);
            for(int row=0; row<3; row++)
            {
                for(int col=0; col<3; col++)
                {
                    // 矩形描画
                    gfx.DrawRectangle(singlePen, new XRect(10+col*80,250+row*40,80,40));
                }
            }


            // Save the document...
            var fullName = PdfFileUtility.GetTempPdfFullFileName($"HelloWorld_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
            document.Save(fullName);
        }
    }
}
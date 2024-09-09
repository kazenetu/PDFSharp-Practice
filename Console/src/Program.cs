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
            var rect = new XRect((width -200)/2,10,200,40);

            // 矩形描画
            gfx.DrawRectangle(pen, rect);

            // 文字描画
            rect.X += 5;
            rect.Y += 5;
            rect.Height -= 5;
            rect.Width -= 5;
            var tf = new XTextFormatter(gfx);
            gfx.DrawString("注文書", font, XBrushes.Black, rect, XStringFormats.Center);

            // 表形式描画確認
            var singlePen = new XPen(XColors.Gray, 1);
            var headerBrush = XBrushes.Blue;
            // 日本語フォント設定
            var gridFont = new XFont("Gen Shin Gothic", 10, XFontStyleEx.BoldItalic, new XPdfFontOptions(PdfFontEmbedding.EmbedCompleteFontFile));
            var headerNames = new List<string> {"No.","製品名","単価","数量","金額"};
            var headerWidth = new List<int> { 25, 250, 80, 50, 150 };

            // 注文データサンプル
            List<Order> Orders = new List<Order>()
            {
                new Order(1,"商品A",100,1,100),
                new Order(2,"商品B",1000,3,3000),
                new Order(3,"商品C",200,1,200),
                new Order(4,"商品D",500,10,5000),
            };

            var startX = (((int)page.Width.Value) - headerWidth.Sum()) /2;
            var y = 100;
            var isDrawHeader = false;
            foreach (var order in Orders)
            {
                var addY = 0;

                // 描画開始Xを設定
                var x = startX;

                // ヘッダ
                if(!isDrawHeader)
                {
                    addY = 20;
                    // ヘッダ
                    for (int col = 0; col < headerWidth.Count; col++)
                    {
                        var gridRect = new XRect(x, y, headerWidth[col], 20);
                        gfx.DrawRectangle(singlePen, headerBrush, gridRect);
                        gfx.DrawString($"{headerNames[col]}", gridFont, XBrushes.White, gridRect, XStringFormats.Center);
                        x += headerWidth[col];
                    }
                    y += addY;
                    isDrawHeader = true;

                    // Xの初期化
                    x = startX;
                }

                // データ
                addY = 20;
                for (int col = 0; col < headerWidth.Count; col++)
                {
                    var gridRect = new XRect(x, y, headerWidth[col], 20);
                    gfx.DrawRectangle(singlePen, gridRect);
                    gridRect.X += 5;
                    gridRect.Width -= 10;
                    gfx.DrawString(GetString(order,col), gridFont, XBrushes.Black, gridRect, XStringFormats.Center);
                    x += headerWidth[col];
                }
                y += addY;
            }


            // Save the document...
            var fullName = PdfFileUtility.GetTempPdfFullFileName($"HelloWorld_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
            document.Save(fullName);
        }

        /// <summary>
        /// カラム番号によって文字列を返す
        /// </summary>
        /// <param name="order">注文情報</param>
        /// <param name="colIndex">カラム番号</param>
        /// <returns>対象の文字列</returns>
        public static string GetString(Order order, int colIndex)
        {
            switch(colIndex)
            {
                case 0:
                    return order.No.ToString();
                case 1:
                    return order.ProductName;
                case 2:
                    return order.unitPrice.ToString("C");
                case 3:
                    return order.Qty.ToString("#,#");
                case 4 :
                    return order.TotalPrice.ToString("C");
                default:
                        return string.Empty;
            };
        }


        /// <summary>
        /// 注文情報
        /// </summary>
        /// <param name="No">No.</param>
        /// <param name="ProductName">製品名</param>
        /// <param name="unitPrice">単価</param>
        /// <param name="Qty">数量</param>
        /// <param name="TotalPrice">金額</param>
        public record Order(int No, string ProductName, decimal unitPrice, decimal Qty, decimal TotalPrice);
    }
}
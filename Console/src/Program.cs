using PdfLibrary;
using PdfLibrary.Interfaces;
using PdfLibrary.Layout;
using PdfLibrary.DataLists;

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
            PdfLibrary.Utilites.Utility.SetJapaneseFontResolver();

            // 注文データサンプル
            var no = 1;
            List<IData> orders = new List<IData>()
            {
                new Order(no++,"商品A",100,1),
                new Order(no++,"商品B",1000,3),
                new Order(no++,"商品C",200,1),
                new Order(no++,"商品D",500,10),
            };
            var pdfMain = new PdfMain(new OrderLayout(), orders);

            //ファイル作成
            var fullName = PdfLibrary.Utilites.Utility.GetFullPath($"OrderReport_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
            using(var fileStream = File.Create(fullName)){
                pdfMain.Create(fileStream);
            }
        }
   }
}
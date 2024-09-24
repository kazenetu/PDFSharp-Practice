using PdfSharp.Pdf;

namespace PdfLibrary.Utilites
{
    /// <summary>
    /// ユーティリティクラス
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 日本語フォントリゾルバーのグローバル登録
        /// </summary>
        public static void SetJapaneseFontResolver()
        {
            PdfSharp.Fonts.GlobalFontSettings.FontResolver = new JapaneseFontResolver();
        }
    }
}
using PdfLibrary.Interfaces;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace PdfLibrary
{
    /// <summary>
    /// レイアウト種別
    /// </summary>
    public enum LayoutKinds
    {
        None,
        Order,
    }

    /// <summary>
    /// PdfLibraryエントリクラス
    /// </summary>
    public class PdfMain
    {
        /// <summary>
        /// 帳票レイアウト
        /// </summary>
        private ILayout _layout;

        /// <summary>
        /// 出力用データリスト
        /// </summary>
        private List<IData> _dataList;

        /// <summary>
        /// PDFsharpドキュメントインスタンス
        /// </summary>
        private PdfDocument _document;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="layout">帳票レイアウト</param>
        /// <param name="dataList">出力用データリスト</param>
        public PdfMain(ILayout layout, List<IData> dataList)
        {
            _layout = layout;
            _dataList = dataList;

            _document = new PdfDocument();
        }

        /// <summary>
        /// パラメータ「Stream」にPDF書き込み
        /// </summary>
        /// <param name="stream">出力用Stream</param>
        public void Create(Stream stream)
        {
            //帳票レイアウト書き込み
            if(!_layout.Create(_document, _dataList))
                throw new Exception("帳票レイアウト失敗");

            // 出力用Streamに出力
            _document.Save(stream);
        }

    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Quality;

namespace webapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult GetPdf()
    {
        try
        {
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
    
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document...
                document.Save(ms);

                // save to stream as PDF
                return File(ms.GetBuffer(), "application/pdf");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

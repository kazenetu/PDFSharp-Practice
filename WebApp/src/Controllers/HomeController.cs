using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using PdfLibrary;
using PdfLibrary.Interfaces;
using PdfLibrary.DataLists;

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
            // 注文データサンプル
            var no = 1;
            List<IData> orders = new List<IData>()
            {
                new Order(no++,"商品A",100,1),
                new Order(no++,"商品B",1000,3),
                new Order(no++,"商品C",200,1),
                new Order(no++,"商品D",500,10),
            };
            var pdfMain = new PdfMain(LayoutKinds.Order, orders);

            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document...
                pdfMain.Create(ms);

                // save to stream as PDF
                return File(ms.GetBuffer(), "application/pdf");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}

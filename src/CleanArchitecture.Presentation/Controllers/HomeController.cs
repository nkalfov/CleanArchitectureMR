using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Presentation.Models;
using CleanArchitecture.Presentation.Resources;

namespace CleanArchitecture.Presentation.Controllers;

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

    [Route(CommonRoutes.Error)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var model = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };

        return View(model);
    }

    [Route(CommonRoutes.NotFound)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult ErrorNotFound()
    {
        var path = HttpContext.Items[HttpContextItems.RequestOrigin] as string;

        if (!string.IsNullOrWhiteSpace(path))
            ViewBag.OriginalPath = string.Concat("at ", path, " ");

        return View();
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        public ProductsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}


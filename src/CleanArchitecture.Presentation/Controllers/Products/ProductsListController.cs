
using CleanArchitecture.Application.Products.Queries.GetProductList;
using CleanArchitecture.Presentation.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers.Products
{
    public class ProductsListController : Controller
    {
        private readonly IGetProductsListQuery _getProductsListQuery;

        public ProductsListController(
            IGetProductsListQuery getProductsListQuery)
        {
            _getProductsListQuery = getProductsListQuery;
        }

        [HttpGet(CommonRoutes.ProductsList)]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = PageTitles.ProductsIndex;

            var products = await _getProductsListQuery.ExecuteAsync();

            return View(products);
        }
    }
}


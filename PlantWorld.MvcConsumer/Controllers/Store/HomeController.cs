using Microsoft.AspNetCore.Mvc;
using PlantWorld.MvcConsumer.Models;
using PlantWorld.MvcConsumer.Services.Interfaces;
using System.Diagnostics;

namespace PlantWorld.MvcConsumer.Controllers.Store
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

    }
}

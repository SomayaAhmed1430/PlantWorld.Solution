using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlantWorld.MvcConsumer.Models.ProductDTOs;
using PlantWorld.MvcConsumer.Services.Implementations;
using PlantWorld.MvcConsumer.Services.Interfaces;

namespace PlantWorld.MvcConsumer.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // Get: Product/Index
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // Get: Product/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }


        // Post: Product/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO productDto, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(productDto);
            }

            // رفع الصورة (زي ما عملنا قبل كده)
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                productDto.ImgUrl = "/images/products/" + fileName;
            }

            await _productService.AddAsync(productDto);

            TempData["AlertMessage"] = "تم إضافة المنتج بنجاح!";
            TempData["AlertType"] = "success";

            return RedirectToAction(nameof(Index));
        }


    }
}

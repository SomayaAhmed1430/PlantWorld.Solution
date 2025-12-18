using Microsoft.AspNetCore.Mvc;
using PlantWorld.MvcConsumer.Models.CategoryDTOs;
using PlantWorld.MvcConsumer.Services.Interfaces;

namespace PlantWorld.MvcConsumer.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // GET: Category/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateDTO categoryDto, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
                return View(categoryDto);

            // رفع الصورة
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/categories");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                // نخزن المسار
                categoryDto.ImgUrl = "/images/categories/" + fileName;
            }

            // نبعت للـ API
            await _categoryService.AddAsync(categoryDto);

            return RedirectToAction(nameof(Index));

        }
    }
}

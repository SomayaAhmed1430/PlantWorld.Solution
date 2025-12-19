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

        // GET: Category/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var categoryUpdateDto = new CategoryUpdateDTO
            {
                Id = category.Id,
                Name = category.Name,
                ImgUrl = category.ImgUrl,
                Description = category.Description
            };

            return View(categoryUpdateDto);
        }

        // POST: Category/Edit/id
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryUpdateDTO categoryDto, IFormFile? imageFile)
        {
            if (id != categoryDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(categoryDto);

            // نجيب الكاتيجوري القديمة
            var existingCategory = await _categoryService.GetByIdAsync(id);
            if (existingCategory == null)
                return NotFound();

            // لو في صورة جديدة
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/categories");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                categoryDto.ImgUrl = "/images/categories/" + fileName;
            }
            else
            {
                // مفيش صورة → نحتفظ بالقديمة
                categoryDto.ImgUrl = existingCategory.ImgUrl;
            }

            // نبعت للـ API
            await _categoryService.UpdateAsync(id, categoryDto);

            return RedirectToAction(nameof(Index));
        }


        // GET: Category/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }


        // POST: Category/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

        // GET: Category/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }
    }
}
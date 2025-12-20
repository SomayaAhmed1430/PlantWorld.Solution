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

            var vm = new ProductCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(vm);
        }


        // Post: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel vm, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                vm.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return View(vm);
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

                vm.Product.ImgUrl = "/images/products/" + fileName;
            }

            await _productService.AddAsync(vm.Product);

            TempData["AlertMessage"] = "تم إضافة المنتج بنجاح";
            TempData["AlertType"] = "success";

            return RedirectToAction(nameof(Index));

        }


        // Get: Product/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            var categories = await _categoryService.GetAllAsync();

            var vm = new ProductUpdateViewModel
            {
                Product = new ProductUpdateDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    ImgUrl = product.ImgUrl
                },
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };
            return View(vm);
        }

        // Post: Product/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductUpdateViewModel vm, IFormFile imageFile)
        {
            if (id != vm.Product.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                vm.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
                return View(vm);
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
                vm.Product.ImgUrl = "/images/products/" + fileName;
            }

            await _productService.UpdateAsync(id, vm.Product);

            TempData["AlertMessage"] = "تم تحديث المنتج بنجاح";
            TempData["AlertType"] = "success";

            return RedirectToAction(nameof(Index));
        }

        // Get: Product/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();

            return View(product);
        }

        // Post: Product/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);

            TempData["AlertMessage"] = "تم حذف المنتج بنجاح";
            TempData["AlertType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        // Get: Product/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();

            return View(product);
        }
    }
}

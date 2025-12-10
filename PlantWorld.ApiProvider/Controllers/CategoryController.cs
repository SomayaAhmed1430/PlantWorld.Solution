using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantWorld.ApiProvider.DTOs.CategoryDTOs;
using PlantWorld.ApiProvider.Models;
using PlantWorld.ApiProvider.Repositories.Interfaces;
using System.Net.Sockets;

namespace PlantWorld.ApiProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepo.GetAllAsync();

            var categoriesDto = categories.Select(c => new CategoryWithProductCountDTO
            {
                Id = c.Id,
                Name = c.Name,
                ImgUrl = c.ImgUrl,
                Description = c.Description,
                ProductCount = c.Products?.Count() ?? 0
            });

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoriesDto = new CategoryWithProductCountDTO
            {
                Id = category.Id,
                Name = category.Name,
                ImgUrl = category.ImgUrl,
                Description = category.Description,
                ProductCount = category.Products?.Count() ?? 0
            };

            return Ok(categoriesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                ImgUrl = categoryDto.ImgUrl,
                Description = categoryDto.Description
            };

            var createdCategory = await _categoryRepo.AddAsync(category);

            var createdCategoryDto = new CategoryWithProductCountDTO
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                ImgUrl = createdCategory.ImgUrl,
                Description = createdCategory.Description,
                ProductCount = 0
            };

            return CreatedAtAction(nameof(GetById),
                new { id = createdCategory.Id },
                createdCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDTO categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest();

            var categoryExists = await _categoryRepo.GetByIdAsync(id);

            if (categoryExists == null)
            {
                return NotFound();
            }

            var category = new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                ImgUrl = categoryDto.ImgUrl,
                Description = categoryDto.Description
            };


            var updatedCategory = await _categoryRepo.UpdateAsync(category);


            var updatedCategoryDto = new CategoryWithProductCountDTO
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                ImgUrl = updatedCategory.ImgUrl,
                Description = updatedCategory.Description,
                ProductCount = updatedCategory.Products?.Count() ?? 0
            };

            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryRepo.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantWorld.ApiProvider.DTOs.ProductDTOs;
using PlantWorld.ApiProvider.Models;
using PlantWorld.ApiProvider.Repositories.Interfaces;

namespace PlantWorld.ApiProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepo.GetAllAsync();

            var productsDto = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                ImgUrl = p.ImgUrl,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            });

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                ImgUrl = product.ImgUrl,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name
            };

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Name = productDto.Name,
                ImgUrl = productDto.ImgUrl,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
            };

            var createdProduct = await _productRepo.AddAsync(product);

            var productWithCategory = await _productRepo.GetByIdAsync(createdProduct.Id);

            var createdProductDto = new ProductDTO
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                ImgUrl = createdProduct.ImgUrl,
                Description = createdProduct.Description,
                Price = createdProduct.Price,
                CategoryId = createdProduct.CategoryId,
                CategoryName = createdProduct.Category?.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProductDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO productDto)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (id != product.Id)
            {
                return BadRequest();
            }

            product.Name = productDto.Name;
            product.ImgUrl = productDto.ImgUrl;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;

            var updatedProduct = await _productRepo.UpdateAsync(product);

            return Ok(new ProductUpdateDTO
            {
                Name = updatedProduct.Name,
                ImgUrl = updatedProduct.ImgUrl,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                CategoryId = updatedProduct.CategoryId,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productRepo.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

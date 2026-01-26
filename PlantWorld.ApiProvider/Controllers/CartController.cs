using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantWorld.ApiProvider.DTOs.CartDTOs;
using PlantWorld.ApiProvider.Models;
using PlantWorld.ApiProvider.Repositories.Interfaces;

namespace PlantWorld.ApiProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public CartController(ICartRepository cartRepo, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        // Get: api/cart/{sessionId}  
        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetCart(string sessionId)
        {
            var items = (await _cartRepo.GetAllAsync())
                        .Where(c => c.SessionId == sessionId)
                        .Select(c => new CartItemDTO
                        {
                            ProductId = c.ProductId,
                            ProductName = c.Product?.Name ?? "",
                            Price = c.Product?.Price ?? 0,
                            Quantity = c.Quantity,
                            TotalPrice = c.TotalPrice
                        }).ToList();

            var cartDto = new CartDTO
            {
                SessionId = sessionId,
                Items = items
            };

            return Ok(cartDto);
        }


        //POST: api/cart 
        [HttpPost]
        public async Task<IActionResult> AddToCart(CartCreateDTO createDto)
        {
            var product = await _productRepo.GetByIdAsync(createDto.ProductId);
            if (product == null) return BadRequest($"Product with id {createDto.ProductId} not found");

            var cartItem = new Cart
            {
                SessionId = createDto.SessionId,
                ProductId = product.Id,
                Quantity = createDto.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            await _cartRepo.AddAsync(cartItem);
            return Ok(new { message = "Item added to cart successfully" });
        }


        // DELETE: api/cart/{id}   
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            await _cartRepo.RemoveAsync(id);
            return Ok(new { message = "Item removed from cart" });
        }

        // DELETE: api/cart/clear/{sessionId}
        [HttpDelete("clear/{sessionId}")]
        public async Task<IActionResult> ClearCart(string sessionId)
        {
            await _cartRepo.ClearAsync(sessionId);
            return Ok(new { message = "Cart cleared" });
        }
    }
}

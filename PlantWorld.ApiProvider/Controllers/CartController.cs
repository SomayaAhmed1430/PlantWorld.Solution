using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantWorld.ApiProvider.DTOs.CartDTOs;
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
    }
}

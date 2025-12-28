using Microsoft.AspNetCore.Mvc;
using PlantWorld.ApiProvider.DTOs.CheckoutDTOs;
using PlantWorld.ApiProvider.Models;
using PlantWorld.ApiProvider.Repositories.Interfaces;

namespace PlantWorld.ApiProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutRepository _checkoutRepo;
        private readonly IProductRepository _productRepo;

        public CheckoutController(
            ICheckoutRepository checkoutRepo,IProductRepository productRepo)
        {
            _checkoutRepo = checkoutRepo;
            _productRepo = productRepo;
        }

        // POST: api/Checkout  (Create Order)
        [HttpPost]
        public async Task<IActionResult> Create(CheckoutCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Items == null || !dto.Items.Any())
                return BadRequest("Order must contain at least one item");

            var checkout = new Checkout
            {
                Name = dto.Name,
                Phone = dto.Phone,
                City = dto.City,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);

                if (product == null)
                    return BadRequest($"Product with id {item.ProductId} not found");

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                totalAmount += product.Price * item.Quantity;
                checkout.OrderItems.Add(orderItem);
            }

            checkout.TotalAmount = totalAmount;

            await _checkoutRepo.CreateAsync(checkout);

            return Ok(new
            {
                message = "Order created successfully",
                orderId = checkout.Id
            });
        }

        


    }
}
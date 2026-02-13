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
        private readonly ICartRepository _cartRepo;

        public CheckoutController(
            ICheckoutRepository checkoutRepo, IProductRepository productRepo, ICartRepository cartRepo)
        {
            _checkoutRepo = checkoutRepo;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
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


        // GET: api/Checkout  (Get All Orders => Admin)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _checkoutRepo.GetAllAsync();

            var checkoutDTOs = orders.Select(c => new CheckoutDetailsDTO
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                City = c.City,
                Address = c.Address,
                CreatedAt = c.CreatedAt,
                TotalAmount = c.TotalAmount,
                Status = c.Status.ToString(),
                Items = c.OrderItems?.Select(oi => new CheckoutItemDetailsDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    ProductImageUrl = oi.Product.ImgUrl,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList() ?? new List<CheckoutItemDetailsDTO>()
            }).ToList();
            return Ok(checkoutDTOs);
        }


        // GET: api/Checkout/{id}  (Get Order By Id)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _checkoutRepo.GetByIdAsync(id);

            if (order == null)
                return NotFound($"Order with id {id} not found");

            var checkoutDTO = new CheckoutDetailsDTO
            {
                Id = order.Id,
                Name = order.Name,
                Phone = order.Phone,
                City = order.City,
                Address = order.Address,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                Items = order.OrderItems?.Select(oi => new CheckoutItemDetailsDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList() ?? new List<CheckoutItemDetailsDTO>()
            };
            return Ok(checkoutDTO);
        }


        // PUT: api/Checkout/{id}/status  (Update Order Status => Admin)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateCheckoutStatusDTO dto)
        {
            var order = await _checkoutRepo.GetByIdAsync(id);

            if (order == null)
                return NotFound($"Order with id {id} not found");
            
            var updated = await _checkoutRepo.UpdateStatusAsync(id, dto.Status);
           
            if (!updated)
                return StatusCode(500, "Failed to update order status");
            
            return Ok(new { message = "Order status updated successfully" });
        }


        // POST: api/Checkout/from-cart  (Create Order from Cart)
        [HttpPost("from-cart")]
        public async Task<IActionResult> CreateFromCart(CheckoutFromCartDTO dto)
        {
            var cartItems = (await _cartRepo.GetAllAsync())
                            .Where(c => c.SessionId == dto.SessionId)
                            .ToList();

            if (!cartItems.Any())
                return BadRequest("Cart is empty");

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

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                totalAmount += orderItem.Price * orderItem.Quantity;
                checkout.OrderItems.Add(orderItem);
            }

            checkout.TotalAmount = totalAmount;

            await _checkoutRepo.CreateAsync(checkout);

            // نفرغ الكارت
            await _cartRepo.ClearAsync(dto.SessionId);

            return Ok(new
            {
                message = "Order created successfully",
                orderId = checkout.Id
            });
        }

    }
}
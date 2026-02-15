using Microsoft.AspNetCore.Mvc;
using PlantWorld.MvcConsumer.Services.Interfaces;

namespace PlantWorld.MvcConsumer.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(string? status)
        {
            var orders = await _orderService.GetAllAsync();

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.Status == status).ToList();
            }

            return View(orders);
        }
    }
}

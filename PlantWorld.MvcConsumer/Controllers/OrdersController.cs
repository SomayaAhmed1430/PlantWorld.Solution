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


        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var result = await _orderService.UpdateStatusAsync(id, status);

            if (!result)
            {
                return Content("فشل تحديث الحالة ❌");
            }

            return RedirectToAction("Details", new { id });
        }


    }
}

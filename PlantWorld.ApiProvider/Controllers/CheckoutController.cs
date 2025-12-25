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
    }
}
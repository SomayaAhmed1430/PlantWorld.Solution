using PlantWorld.ApiProvider.Models;

namespace PlantWorld.ApiProvider.DTOs.CheckoutDTOs
{
    public class UpdateCheckoutStatusDTO
    {
        public int CheckoutId { get; set; }
        public OrderStatus Status { get; set; }
    }
}

namespace PlantWorld.ApiProvider.DTOs.CartDTOs
{
    public class CartDTO
    {
        public string SessionId { get; set; } = string.Empty;
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    }
}

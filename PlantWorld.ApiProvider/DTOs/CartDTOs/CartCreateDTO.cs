namespace PlantWorld.ApiProvider.DTOs.CartDTOs
{
    public class CartCreateDTO
    {
        public string SessionId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

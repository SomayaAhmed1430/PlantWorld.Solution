namespace PlantWorld.ApiProvider.DTOs.CheckoutDTOs
{
    public class CheckoutDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public List<CheckoutItemDetailsDTO> Items { get; set; } = new();
    }
}

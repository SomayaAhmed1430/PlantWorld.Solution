namespace PlantWorld.ApiProvider.DTOs.CheckoutDTOs
{
    public class CheckoutCreateDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


        public List<CheckoutItemDTO> Items { get; set; } = new();
    }
}

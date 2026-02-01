namespace PlantWorld.ApiProvider.DTOs.CheckoutDTOs
{
    public class CheckoutFromCartDTO
    {
        public string SessionId { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}

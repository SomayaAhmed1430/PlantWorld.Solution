namespace PlantWorld.ApiProvider.DTOs.ProductDTOs
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}

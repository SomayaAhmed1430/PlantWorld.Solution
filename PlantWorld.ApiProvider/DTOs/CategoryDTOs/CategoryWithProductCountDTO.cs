namespace PlantWorld.ApiProvider.DTOs.CategoryDTOs
{
    public class CategoryWithProductCountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public int? ProductCount { get; set; }
    }
}

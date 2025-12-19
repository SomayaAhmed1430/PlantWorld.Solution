namespace PlantWorld.MvcConsumer.Models.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }


        public IFormFile? ImageFile { get; set; }

    }
}

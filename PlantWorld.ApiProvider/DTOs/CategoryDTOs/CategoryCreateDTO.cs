using System.ComponentModel.DataAnnotations;

namespace PlantWorld.ApiProvider.DTOs.CategoryDTOs
{
    public class CategoryCreateDTO
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
    }
}

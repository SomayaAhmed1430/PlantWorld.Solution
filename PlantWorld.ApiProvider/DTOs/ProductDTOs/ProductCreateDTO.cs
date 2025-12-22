using System.ComponentModel.DataAnnotations;

namespace PlantWorld.ApiProvider.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}

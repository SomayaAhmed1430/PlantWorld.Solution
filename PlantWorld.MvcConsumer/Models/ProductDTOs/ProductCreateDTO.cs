using System.ComponentModel.DataAnnotations;

namespace PlantWorld.MvcConsumer.Models.ProductDTOs
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        [MinLength(3, ErrorMessage = "اسم المنتج لا يقل عن 3 حروف")]
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(1, 100000, ErrorMessage = "السعر غير صحيح")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "اختر القسم")]
        public int CategoryId { get; set; }
    }
}

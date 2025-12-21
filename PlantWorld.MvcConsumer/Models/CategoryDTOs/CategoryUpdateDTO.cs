using System.ComponentModel.DataAnnotations;

namespace PlantWorld.MvcConsumer.Models.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم القسم مطلوب")]
        [MinLength(3, ErrorMessage = "اسم القسم لا يقل عن 3 حروف")]
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }


        public IFormFile? ImageFile { get; set; }

    }
}

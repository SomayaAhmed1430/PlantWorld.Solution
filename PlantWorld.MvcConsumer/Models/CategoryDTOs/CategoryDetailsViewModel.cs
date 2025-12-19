using PlantWorld.MvcConsumer.Models.ProductDTOs;

namespace PlantWorld.MvcConsumer.Models.CategoryDTOs
{
    public class CategoryDetailsViewModel
    {
        public CategoryWithProductCountDTO Category { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }

}

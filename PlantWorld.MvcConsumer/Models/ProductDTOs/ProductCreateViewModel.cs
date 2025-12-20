using Microsoft.AspNetCore.Mvc.Rendering;

namespace PlantWorld.MvcConsumer.Models.ProductDTOs
{
    public class ProductCreateViewModel
    {
        public ProductCreateDTO Product { get; set; } = new ProductCreateDTO();

        public IEnumerable<SelectListItem> Categories { get; set; }
            = Enumerable.Empty<SelectListItem>();
    }
}

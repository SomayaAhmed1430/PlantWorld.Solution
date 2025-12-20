using Microsoft.AspNetCore.Mvc.Rendering;

namespace PlantWorld.MvcConsumer.Models.ProductDTOs
{
    public class ProductUpdateViewModel
    {
            public ProductUpdateDTO Product { get; set; } = new();
            public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace PlantWorld.ApiProvider.Models
{
    public class Cart
    {
        public int Id { get; set; }


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * Product.Price;
        public DateTime CreatedAt { get; set; }
    }
}

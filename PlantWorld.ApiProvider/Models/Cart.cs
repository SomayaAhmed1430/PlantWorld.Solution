using System.ComponentModel.DataAnnotations.Schema;

namespace PlantWorld.ApiProvider.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string SessionId { get; set; } 

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int Quantity { get; set; }
        public decimal TotalPrice => Product != null ? Quantity * Product.Price : 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace PlantWorld.ApiProvider.Models
{
    public class OrderItem
    {
        public int Id { get; set; }


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int Quantity { get; set; }
        public decimal Price { get; set; }


        [ForeignKey("Checkout")]
        public int CheckoutId { get; set; }
        public Checkout? Checkout { get; set; }
    }
}

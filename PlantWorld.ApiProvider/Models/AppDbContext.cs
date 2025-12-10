using Microsoft.EntityFrameworkCore;

namespace PlantWorld.ApiProvider.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CareGuide> CareGuides { get; set; }


    }
}

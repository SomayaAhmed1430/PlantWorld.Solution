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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Checkout>()
                .Property(c => c.TotalAmount)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

    }
}

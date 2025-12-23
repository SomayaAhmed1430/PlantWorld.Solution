using Microsoft.EntityFrameworkCore;
using PlantWorld.ApiProvider.Models;
using PlantWorld.ApiProvider.Repositories.Interfaces;

namespace PlantWorld.ApiProvider.Repositories.Implementations
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly AppDbContext _context;
        public CheckoutRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Checkout> CreateAsync(Checkout checkout)
        {
            checkout.CreatedAt = DateTime.UtcNow;
            checkout.Status = OrderStatus.Pending;

            _context.Checkouts.Add(checkout);
            await _context.SaveChangesAsync();

            return checkout;
        }

        public async Task<IEnumerable<Checkout>> GetAllAsync()
        {
            return await _context.Checkouts
                .Include(c => c.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Checkout?> GetByIdAsync(int id)
        {
            return await _context.Checkouts
                .Include(c => c.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateStatusAsync(int id, OrderStatus status)
        {
            var checkout = await _context.Checkouts.FindAsync(id);

            if (checkout == null)
            {
                return false;
            }

            checkout.Status = status;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}

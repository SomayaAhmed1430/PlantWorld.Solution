using Microsoft.EntityFrameworkCore;
using PlantWorld.ApiProvider.Models;
using PlantWorld.ApiProvider.Repositories.Interfaces;

namespace PlantWorld.ApiProvider.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync(string sessionId)
        {
            var items = await _context.Carts
                        .Where(c => c.SessionId == sessionId)
                        .ToListAsync();

            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _context.Carts
                                 .Include(c => c.Product)
                                 .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Carts
                .Include(c => c.Product)
                .ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var item = await _context.Carts.FindAsync(id);

            if (item != null)
            {
                _context.Carts.Remove(item);
                await _context.SaveChangesAsync();
            }
        }


    }
}

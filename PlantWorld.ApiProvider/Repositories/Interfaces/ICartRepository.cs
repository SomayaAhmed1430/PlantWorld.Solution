using PlantWorld.ApiProvider.Models;

namespace PlantWorld.ApiProvider.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(Cart cart);
        Task<Cart?> GetByIdAsync(int id);
        Task<IEnumerable<Cart>> GetAllAsync();
        Task RemoveAsync(int id);
        Task ClearAsync(string sessionId);
    }

}

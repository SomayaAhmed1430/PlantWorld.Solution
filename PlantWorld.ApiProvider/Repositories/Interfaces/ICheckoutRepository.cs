using PlantWorld.ApiProvider.Models;

namespace PlantWorld.ApiProvider.Repositories.Interfaces
{
    public interface ICheckoutRepository
    {
        Task<Checkout> CreateAsync(Checkout checkout);
        Task<Checkout?> GetByIdAsync(int id);
        Task<IEnumerable<Checkout>> GetAllAsync();
        Task<bool> UpdateStatusAsync(int id, OrderStatus status);
    }
}

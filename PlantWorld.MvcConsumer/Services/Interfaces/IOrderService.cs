using PlantWorld.MvcConsumer.Models.OrderDTOs;

namespace PlantWorld.MvcConsumer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO?> GetByIdAsync(int id);
        Task<bool> UpdateStatusAsync(int id, string status);
    }
}

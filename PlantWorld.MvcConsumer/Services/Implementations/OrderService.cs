using PlantWorld.MvcConsumer.Models.OrderDTOs;
using PlantWorld.MvcConsumer.Services.Interfaces;
using System.Net.Http.Json;

namespace PlantWorld.MvcConsumer.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OrderDTO>>("api/Checkout");
            return response ?? new List<OrderDTO>();
        }

        public async Task<OrderDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<OrderDTO>($"api/Checkout/{id}");
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var dto = new UpdateOrderStatusDTO
            {
                Status = status
            };

            var response = await _httpClient
                .PutAsJsonAsync($"api/checkout/{id}/status", dto);

            return response.IsSuccessStatusCode;
        }

    }
}

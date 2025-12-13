using PlantWorld.MvcConsumer.Models.CategoryDTOs;
using PlantWorld.MvcConsumer.Services.Interfaces;

namespace PlantWorld.MvcConsumer.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task AddAsync(CategoryCreateDTO categoryDto)
        {
            await _httpClient.PostAsJsonAsync("api/Category", categoryDto);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Category/{id}");
        }

        public async Task<IEnumerable<CategoryWithProductCountDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryWithProductCountDTO>>("api/Category") 
                ?? Enumerable.Empty<CategoryWithProductCountDTO>();
        }

        public async Task<CategoryWithProductCountDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryWithProductCountDTO?>($"api/Category/{id}");
        }

        public async Task UpdateAsync(int id, CategoryUpdateDTO categoryDto)
        {
            await _httpClient.PutAsJsonAsync($"api/Category/{id}", categoryDto);
        }
    }
}

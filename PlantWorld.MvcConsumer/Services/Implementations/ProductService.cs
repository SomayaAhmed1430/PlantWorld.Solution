using PlantWorld.MvcConsumer.Models.ProductDTOs;
using PlantWorld.MvcConsumer.Services.Interfaces;
using System.Net.Http;

namespace PlantWorld.MvcConsumer.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAsync(ProductCreateDTO productDto)
        {
            await _httpClient.PostAsJsonAsync("api/Product", productDto);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Product/{id}");
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>("api/Product")
                ?? Enumerable.Empty<ProductDTO>();
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDTO?>($"api/Product/{id}");
        }

        public async Task UpdateAsync(int id, ProductUpdateDTO productDto)
        {
            await _httpClient.PutAsJsonAsync($"api/Product/{id}", productDto);
        }



        public async Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryId)
        {
            return await _httpClient
                .GetFromJsonAsync<IEnumerable<ProductDTO>>($"api/Product/ByCategory/{categoryId}")
                ?? Enumerable.Empty<ProductDTO>();
        }

    }
}

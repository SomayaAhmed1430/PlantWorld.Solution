using PlantWorld.MvcConsumer.Models.ProductDTOs;

namespace PlantWorld.MvcConsumer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByIdAsync(int id);
        Task AddAsync(ProductCreateDTO productDto);
        Task UpdateAsync(int id, ProductUpdateDTO productDto);
        Task DeleteAsync(int id);


        Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryId);

    }
}

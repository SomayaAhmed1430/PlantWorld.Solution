using PlantWorld.MvcConsumer.Models.CategoryDTOs;

namespace PlantWorld.MvcConsumer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryWithProductCountDTO>> GetAllAsync();
        Task<CategoryWithProductCountDTO?> GetByIdAsync(int id);
        Task AddAsync(CategoryCreateDTO categoryDto);
        Task UpdateAsync(int id, CategoryUpdateDTO categoryDto);
        Task DeleteAsync(int id);
    }
}

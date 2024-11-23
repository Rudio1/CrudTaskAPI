using CrudTaskAPI.Application.Dto;

namespace CrudTaskAPI.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(CategoryCreateDto category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
